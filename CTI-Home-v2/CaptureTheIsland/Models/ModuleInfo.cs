using System.Collections.Generic;

namespace CaptureTheIsland.Models
{
    public class ModuleInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public static IList<ModuleInfo> OfferedModules() => new List<ModuleInfo>
        {
            new() { Name = "Password Cracking", Description = "Work with common hash types and rules to recover passwords safely." },
            new() { Name = "Log Analysis", Description = "Parse web/server logs to detect anomalies and trace attacker actions." },
            new() { Name = "Cryptography", Description = "Decode classical ciphers and recognize modern crypto primitives." },
            new() { Name = "Forensics", Description = "Extract evidence from files, images, and memory captures." },
            new() { Name = "OSINT", Description = "Gather and correlate public data to answer investigative questions." },
            new() { Name = "Networking", Description = "Read packets, follow TCP streams, and understand protocol behavior." },
            new() { Name = "Scripting", Description = "Automate repetitive tasks and data extraction to speed up analysis." }
        };
    }
}
