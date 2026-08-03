# Chromatography Workstation Architecture Report

## 1. Project Overview
The project is a chromatography workstation designed for environmental data analysis, featuring a modular hardware architecture, robust data collection, and a user-friendly interface for monitoring and analyzing VOCs.

## 2. Core Architecture & Modules

### 2.1 Data Collection
- **Edge Collector**: Handles data collection from various devices using different protocols (Modbus, HTTP, TCP, etc.).
- **Real-time Server Hub**: Manages client connections and publishes messages to subscribed clients for real-time data updates.
- **Instrument Drivers**: Modular drivers for temperature control, event/relay, electronic pneumatic, analysis, and instrument control.

### 2.2 Data Analysis
- **Analyzer**: Processes environmental data to detect peaks, calculate concentrations, and perform data validation.
- **Peak Detection**: Identifies local maxima and minima in a trace using a smoothed version of the data.
- **Concentration Calculation**: Matches detected peaks to predefined pollutants based on retention times and calculates concentrations.

### 2.3 User Interface
- **Dashboard**: Displays navigation items, methods, and data for different views (dashboard, chromatogram, method, process, report, settings, debug).
- **Live Chromatogram**: Provides real-time data visualization.
- **Method Management**: Manages analysis methods and configurations.
- **Report Generation**: Generates and prints reports based on analysis results.

## 3. Tech Stack

- **Programming Language**: Go for server-side logic and data processing.
- **Web Technologies**: HTML, CSS, and JavaScript for frontend development.
- **Database**: SQLite for local data storage.
- **Communication Protocols**: Modbus, HTTP, TCP, MQTT for device communication.
- **Third-party Libraries**: Minimalmodbus for Modbus communication, Elasticsearch for data indexing, and IoT networks for data distribution.

## 4. Reusable Components / Highlights

- **Modular Hardware Abstraction Layer (HAL)**: Enables easy integration of different hardware modules and devices.
- **Instrument Drivers**: Provides a standardized interface for controlling various instruments and devices.
- **Real-time Data Processing**: Handles real-time data updates and notifications.
- **Data Analysis Algorithms**: Includes peak detection, concentration calculation, and data validation.
- **User-friendly Interface**: Provides a simple and intuitive user interface for monitoring and analyzing data.