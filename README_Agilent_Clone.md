# Agilent Clone Workstation (Modern Web + Go Architecture)

This project is a modern reconstruction of the classic Agilent ChemStation, utilizing international standards (AnIML, AIA, MQTT, SiLA 2) while strictly adhering to Agilent's classic UX/UI concepts (Integration Events, 4 Classic Views, Method-Driven workflow).

## Architecture

1. **Go Edge API & Analyzer (`src/edge`)**: 
   - `Collector`: Translates legacy GCKC protocol to modern internal standard streams.
   - `Analyzer`: Maps classic Agilent integration events (`Initial Area Reject`, `Peak Width`, `Tangent Skim`) to math engine parameters.
   - `Edge API`: Serves as the REST/WS gateway for the Web UI.

2. **React Web UI (`src/ui/apps/workstation`)**:
   - `ConfigEditor`: Offline hardware configuration.
   - `MethodRun`: System Diagram and Online Plot.
   - `SequenceTable`: Batch processing and dynamic queueing.
   - `DataAnalysis`: Interactive Chromatogram, Integration Events, and Calibration Tables.

## Quick Start

1. Start the Go Edge API & Analyzer:
```bash
cd src/edge
go run ./cmd/edge-api/main.go
```

2. Start the React Frontend:
```bash
cd src/ui/apps/workstation
npm install
npm run dev
```

3. Open your browser to view the Agilent Clone Interface.
