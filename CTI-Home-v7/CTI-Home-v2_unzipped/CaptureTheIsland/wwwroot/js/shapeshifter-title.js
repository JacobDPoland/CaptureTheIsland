// Minimal "Shape Shifter"-style particle text for rotating words.
// No libraries. Keeps drawing inside the canvas and away from the subtitle.
// Inspired by Kenneth Cachi's Shape Shifter demo: https://codepen.io/kennethcachia/pen/nPbyoR

(() => {
    const WORDS = ["CAPTURE", "THE", "ISLAND"];
    const SWITCH_MS = 1500;              // word change interval
    const SAMPLE_STEP = 6;               // pixel sampling density (smaller = more particles)
    const DOT_RADIUS = 2;                // particle dot radius
    const EASING = 0.08;                 // approach speed toward targets
    const JITTER = 0.5;                  // slight randomness to motion
    const BG_CLEAR_ALPHA = 0.18;         // trail effect; lower = longer trails

    const BG_COLOR = "#212529";          // background
    const TEXT_COLOR = "#f5f7fa";        // text and particle color

    const canvas = document.getElementById("title-canvas");
    if (!canvas) return;
    const ctx = canvas.getContext("2d");

    // Offscreen canvas used to rasterize text and pick target points.
    const off = document.createElement("canvas");
    const octx = off.getContext("2d");

    // Resize handling: canvas width follows CSS width; height follows CSS height.
    function sizeCanvasToCSS(cnv) {
        const rect = cnv.getBoundingClientRect();
        const ratio = window.devicePixelRatio || 1;
        const w = Math.max(300, Math.floor(rect.width * ratio));
        const h = Math.max(120, Math.floor(rect.height * ratio));
        if (cnv.width !== w || cnv.height !== h) {
            cnv.width = w;
            cnv.height = h;
        }
        return { w, h, ratio };
    }

    // Particle pool
    const particles = [];

    function makeParticle(w, h) {
        // Start at random edge for a nice in-flow effect
        const edge = Math.floor(Math.random() * 4);
        let x = 0, y = 0;
        if (edge === 0) { x = Math.random() * w; y = -20; }
        else if (edge === 1) { x = w + 20; y = Math.random() * h; }
        else if (edge === 2) { x = Math.random() * w; y = h + 20; }
        else { x = -20; y = Math.random() * h; }

        return { x, y, vx: 0, vy: 0, tx: x, ty: y };
    }

    function ensureParticleCount(n, w, h) {
        while (particles.length < n) particles.push(makeParticle(w, h));
        while (particles.length > n) particles.pop();
    }

    // Build target points for a given word.
    function targetsForWord(word, canvasW, canvasH) {
        off.width = canvasW;
        off.height = canvasH;

        const verticalPad = Math.floor(canvasH * 0.18);
        let fontSize = Math.floor(canvasH - verticalPad * 2);
        if (fontSize < 20) fontSize = 20;

        octx.clearRect(0, 0, off.width, off.height);
        octx.textBaseline = "middle";
        octx.textAlign = "left";

        function measureWithSize(sz) {
            octx.font = `700 ${sz}px system-ui, -apple-system, Segoe UI, Roboto, Arial`;
            return octx.measureText(word);
        }

        // Shrink if too wide
        let metrics = measureWithSize(fontSize);
        const maxWidth = Math.floor(canvasW * 0.88);
        while (metrics.width > maxWidth && fontSize > 12) {
            fontSize -= 2;
            metrics = measureWithSize(fontSize);
        }

        octx.font = `700 ${fontSize}px system-ui, -apple-system, Segoe UI, Roboto, Arial`;

        const textW = octx.measureText(word).width;
        const x = Math.floor((canvasW - textW) / 2);
        const y = Math.floor(canvasH * 0.55);

        // Draw light text on dark background for sampling
        octx.clearRect(0, 0, off.width, off.height);
        octx.fillStyle = TEXT_COLOR;
        octx.fillText(word, x, y);

        const img = octx.getImageData(0, 0, off.width, off.height).data;
        const points = [];
        for (let yy = 0; yy < off.height; yy += SAMPLE_STEP) {
            for (let xx = 0; xx < off.width; xx += SAMPLE_STEP) {
                const idx = (yy * off.width + xx) * 4 + 3;
                if (img[idx] > 180) points.push({ x: xx, y: yy });
            }
        }

        return points;
    }

    // Assign targets to particles
    function retarget(points, w, h) {
        ensureParticleCount(points.length, w, h);
        for (let i = points.length - 1; i > 0; i--) {
            const j = (Math.random() * (i + 1)) | 0;
            [points[i], points[j]] = [points[j], points[i]];
        }
        for (let i = 0; i < particles.length; i++) {
            const p = particles[i];
            const t = points[i % points.length];
            p.tx = t.x + (Math.random() - 0.5) * 0.8;
            p.ty = t.y + (Math.random() - 0.5) * 0.8;
        }
    }

    // Animation
    let lastTS = 0;
    function frame(ts) {
        requestAnimationFrame(frame);
        if (!lastTS) lastTS = ts;
        const { w, h } = sizeCanvasToCSS(canvas);

        // Fill background with dark color
        ctx.fillStyle = BG_COLOR;
        ctx.fillRect(0, 0, w, h);

        // Draw particles
        ctx.beginPath();
        for (const p of particles) {
            const dx = p.tx - p.x;
            const dy = p.ty - p.y;
            p.vx += dx * EASING;
            p.vy += dy * EASING;
            p.vx *= 0.86;
            p.vy *= 0.86;
            p.vx += (Math.random() - 0.5) * JITTER;
            p.vy += (Math.random() - 0.5) * JITTER;
            p.x += p.vx;
            p.y += p.vy;
            ctx.moveTo(p.x + DOT_RADIUS, p.y);
            ctx.arc(p.x, p.y, DOT_RADIUS, 0, Math.PI * 2);
        }
        ctx.fillStyle = TEXT_COLOR;
        ctx.fill();
    }

    // Word cycling
    let wordIndex = 0;
    function applyWord() {
        const { w, h } = sizeCanvasToCSS(canvas);
        const points = targetsForWord(WORDS[wordIndex], w, h);
        retarget(points, w, h);
    }

    // Resize handling
    const debounced = (fn, ms = 150) => {
        let t = 0;
        return () => { clearTimeout(t); t = setTimeout(fn, ms); };
    };
    window.addEventListener("resize", debounced(applyWord, 100));

    // Kickoff
    sizeCanvasToCSS(canvas);
    applyWord();
    requestAnimationFrame(frame);
    setInterval(() => {
        wordIndex = (wordIndex + 1) % WORDS.length;
        applyWord();
    }, SWITCH_MS);
})();
