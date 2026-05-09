<div align="center">

# 🛡️ Smart File Monitoring Service

**An enterprise-grade, real-time security and data integrity tracking system running as a native Windows Service.**

[![Follow on GitHub](https://img.shields.io/github/followers/Ahm3dRamad3n?label=Follow&style=social)](https://github.com/Ahm3dRamad3n)
[![Connect on LinkedIn](https://img.shields.io/badge/Connect-LinkedIn-0077B5?style=social&logo=linkedin)](https://www.linkedin.com/in/ahm3d-ramadan/)
<br>
[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)]()
[![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)]()
[![Windows Service](https://img.shields.io/badge/Windows_Service-0078D7?style=for-the-badge&logo=windows&logoColor=white)]()
[![Security Auditing](https://img.shields.io/badge/Security-Auditing-FF2D20?style=for-the-badge)]()

</div>

---

## 📌 Overview

The **Smart File Monitoring Service** is a robust background tracking system designed to ensure data integrity and provide comprehensive security auditing. Built on the `ServiceBase` class, it runs purely as a native Windows Service. It leverages OS-level event handling to asynchronously capture file system changes (Creations, Deletions, Modifications, and Renames) across entire directory trees with millisecond precision.

---

## ✨ Enterprise-Level Features

* 👁️ **Real-Time Security Tracking:** Instant, event-driven detection of any file modifications without resource-heavy polling, ensuring rapid response to potential threats.
* 📂 **Deep Directory Monitoring:** Recursively monitors complex directory structures, including all nested subdirectories, guaranteeing no hidden changes go unnoticed.
* 📝 **Robust Thread-Safe Logging:** Meticulously records every event with precise timestamps, event types, and absolute file paths. Utilizes thread-safe `StreamWriter` logic to ensure zero data loss during high-concurrency file operations.
* ⚙️ **Asynchronous Event Handling:** Uses configured `FileSystemWatcher` instances to process multiple file changes asynchronously, maintaining peak system performance.
* 🛡️ **System Persistence & Initialization:** Dynamically loads watch paths and filters from configuration files, verifying access permissions before initiating the monitoring lifecycle.

---

## 🛠️ Technical Workflow & Architecture

1. **Initialization:** Validates directory existence and permissions based on `App.config`.
2. **Event Handling:** `watcher.EnableRaisingEvents = true` binds asynchronous listeners to OS-level I/O events.
3. **Persistence:** Executes secure disk-writes and manages periodic log rotation to prevent storage overflow while maintaining full audit trails.

---

## 🚀 Installation

As a native Windows Service, it requires administrative installation:

1. Open **Developer Command Prompt** as Administrator.
2. Navigate to the executable's directory.
3. Run: `InstallUtil.exe FileMonitoringService.exe`
4. Start the service via `services.msc`.
5. Check your configured log destination to view the real-time audit trails.

---

## 📄 License

This project is open-source and available under the **MIT License**.
