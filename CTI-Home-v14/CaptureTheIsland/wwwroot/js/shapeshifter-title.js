
// Shape Shifter title with start/stop controls and localStorage preference.
(function(){
  const STORAGE_KEY = 'ctiAnimations'; // 'on' | 'off'
  const WORDS = ["CAPTURE", "THE", "ISLAND"];
  const SWITCH_MS = 2000;              // word change interval
  const SAMPLE_STEP = 3;               // pixel sampling density (smaller = more particles)
  const DOT_RADIUS = 1;                // particle dot radius
  const EASING = 0.08;                 // approach speed toward targets
  const JITTER = 0.25;                  // slight randomness to motion
  const BG_CLEAR_ALPHA = 0.18;
  const TRAILS = false; // disable translucent background to match page color exactly         // trail effect; lower = longer trails

  const canvas = document.getElementById("title-canvas");
  if (!canvas) return;
  const ctx = canvas.getContext("2d");

  const offscreen = document.createElement("canvas");
  const offctx = offscreen.getContext("2d");

  function sizeCanvasToCSS(c) {
    const dpr = window.devicePixelRatio || 1;
    const rect = c.getBoundingClientRect();
    c.width = Math.max(320, Math.floor(rect.width * dpr));
    c.height = Math.max(160, Math.floor(rect.height * dpr));
    c.style.width = rect.width + "px";
    c.style.height = rect.height + "px";
    ctx.setTransform(dpr, 0, 0, dpr, 0, 0);
  }

  function debounce(fn, wait) {
    let t; return function(...args){ clearTimeout(t); t = setTimeout(()=>fn.apply(this,args), wait); };
  }

  function sampleTextPoints(text) {
    const pad = 20;
    const dpr = window.devicePixelRatio || 1;
    offscreen.width = Math.max(10, Math.floor(canvas.width / dpr));
    offscreen.height = Math.max(10, Math.floor(canvas.height / dpr));
    offctx.clearRect(0,0,offscreen.width, offscreen.height);

    const fontSize = Math.floor(Math.min(offscreen.width * 0.22, offscreen.height * 0.5));
    // adjusted for better fit
    offctx.fillStyle = "#fff";
    offctx.textAlign = "center";
    offctx.textBaseline = "middle";
    offctx.font = `bold ${fontSize}px system-ui, -apple-system, Segoe UI, Roboto, Arial, sans-serif`;
    offctx.fillText(text, offscreen.width/2, offscreen.height/2);

    const points = [];
    const step = SAMPLE_STEP;
    const img = offctx.getImageData(0,0,offscreen.width, offscreen.height).data;
    for (let y = 0; y < offscreen.height; y += step) {
      for (let x = 0; x < offscreen.width; x += step) {
        const i = (y * offscreen.width + x) * 4;
        if (img[i+3] > 128) points.push({x, y});
      }
    }
    return points;
  }

  let dots = [];
  let targets = [];
  let wordIndex = 0;
  let rafId = null;
  let intervalId = null;
  let running = false;

  function ensureDots(count) {
    while (dots.length < count) {
      dots.push({
        x: Math.random() * canvas.width,
        y: Math.random() * canvas.height,
        vx: 0, vy: 0
      });
    }
    if (dots.length > count) dots.length = count;
  }

  function applyWord() {
    const text = WORDS[wordIndex];
    targets = sampleTextPoints(text);
    ensureDots(targets.length);
  }

  function solidClear(){
    const dpr = window.devicePixelRatio || 1;
    const logicalW = canvas.width / dpr;
    const logicalH = canvas.height / dpr;
    const prev = ctx.fillStyle;
    ctx.fillStyle = '#212529';
    ctx.fillRect(0,0,logicalW,logicalH);
    ctx.fillStyle = prev;
}

function frame(){
    const dpr = window.devicePixelRatio || 1;
    const logicalW = canvas.width / dpr;
    const logicalH = canvas.height / dpr;
    if (!running) return;
    if (TRAILS) {
      ctx.fillStyle = `rgba(33,37,41, ${BG_CLEAR_ALPHA})`;
      ctx.fillRect(0, 0, logicalW, logicalH);
    } else {
      ctx.clearRect(0, 0, logicalW, logicalH); // reveal CSS background: #212529
    }

    ctx.fillStyle = "#f5f7fa"; // dot color
    for (let i = 0; i < dots.length; i++) {
      const d = dots[i];
      const t = targets[i] || {x: Math.random()*logicalW, y: Math.random()*logicalH};
      const dx = t.x - d.x;
      const dy = t.y - d.y;
      d.vx = d.vx * 0.9 + dx * EASING + (Math.random()-0.5)*JITTER;
      d.vy = d.vy * 0.9 + dy * EASING + (Math.random()-0.5)*JITTER;
      d.x += d.vx;
      d.y += d.vy;
      ctx.beginPath();
      ctx.arc(d.x, d.y, DOT_RADIUS, 0, Math.PI*2);
      ctx.fill();
    }
    rafId = requestAnimationFrame(frame);
  }

  function start(){
    if (running) return;
    running = true;
    sizeCanvasToCSS(canvas);
    solidClear();
    applyWord();
    rafId = requestAnimationFrame(frame);
    if (intervalId) clearInterval(intervalId);
    intervalId = setInterval(()=>{
      wordIndex = (wordIndex + 1) % WORDS.length;
      applyWord();
    }, SWITCH_MS);
  }

  function stop() {
    running = false;
    if (rafId) cancelAnimationFrame(rafId), rafId = null;
    if (intervalId) clearInterval(intervalId), intervalId = null;
    // clear canvas to avoid ghost trails
    ctx.clearRect(0,0,canvas.width,canvas.height);
  }

  window.addEventListener("resize", debounce(()=>{
    sizeCanvasToCSS(canvas);
    if (running) { solidClear(); applyWord(); }
  }, 100));

  // Expose control
  window.CtiTitle = {
    setRunning(on){ if (on) start(); else stop(); },
    isRunning(){ return running; }
  };

  // Auto-start based on preference (default on)
  const pref = (typeof localStorage !== 'undefined') ? localStorage.getItem(STORAGE_KEY) : 'on';
  if (!pref || pref === 'on') start();
})();
