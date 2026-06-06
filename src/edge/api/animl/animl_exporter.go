package animl

import (
	"encoding/xml"
	"fmt"
	"time"
)

// AnIML represents a simplified AnIML 1.0 Document Structure for Chromatography
type AnIML struct {
	XMLName           xml.Name          `xml:"AnIML"`
	Version           string            `xml:"version,attr"`
	SampleSet         SampleSet         `xml:"SampleSet"`
	ExperimentStepSet ExperimentStepSet `xml:"ExperimentStepSet"`
}

type SampleSet struct {
	Sample []Sample `xml:"Sample"`
}

type Sample struct {
	Name     string `xml:"name,attr"`
	SampleID string `xml:"id,attr"`
}

type ExperimentStepSet struct {
	ExperimentStep []ExperimentStep `xml:"ExperimentStep"`
}

type ExperimentStep struct {
	Name       string `xml:"name,attr"`
	MethodName string `xml:"Method>Name"`
	Result     Result `xml:"Result"`
}

type Result struct {
	SeriesSet SeriesSet `xml:"SeriesSet"`
}

type SeriesSet struct {
	Series []Series `xml:"Series"`
}

type Series struct {
	Name       string       `xml:"name,attr"`
	Unit       string       `xml:"unit,attr"`
	DataValues Float64Array `xml:"Float64Array"`
}

type Float64Array struct {
	Values []string `xml:"v"`
}

// ExportSession converts raw chromatography data to AnIML XML format
func ExportSession(sampleName string, methodName string, times []float64, signals []float64) (string, error) {
	timeStrs := make([]string, len(times))
	for i, v := range times {
		timeStrs[i] = fmt.Sprintf("%.4f", v)
	}

	sigStrs := make([]string, len(signals))
	for i, v := range signals {
		sigStrs[i] = fmt.Sprintf("%.4f", v)
	}

	doc := AnIML{
		Version: "1.0",
		SampleSet: SampleSet{
			Sample: []Sample{
				{Name: sampleName, SampleID: fmt.Sprintf("SMP-%d", time.Now().Unix())},
			},
		},
		ExperimentStepSet: ExperimentStepSet{
			ExperimentStep: []ExperimentStep{
				{
					Name:       "Gas Chromatography Analysis",
					MethodName: methodName,
					Result: Result{
						SeriesSet: SeriesSet{
							Series: []Series{
								{Name: "Time", Unit: "min", DataValues: Float64Array{Values: timeStrs}},
								{Name: "Intensity", Unit: "pA", DataValues: Float64Array{Values: sigStrs}},
							},
						},
					},
				},
			},
		},
	}

	out, err := xml.MarshalIndent(doc, "", "  ")
	if err != nil {
		return "", err
	}
	return xml.Header + string(out), nil
}
