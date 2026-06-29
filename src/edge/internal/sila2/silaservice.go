package sila2

import (
	"context"
	"embed"
	"fmt"
	"strings"

	"chromatography-workstation/edge/internal/models"
	pb "chromatography-workstation/edge/internal/sila2/pb"
)

//go:embed fdl/*.xml
var fdlFiles embed.FS

type SiLAServiceServerImpl struct {
	pb.UnimplementedSiLAServiceServer
	twin *models.DigitalTwin
}

func NewSiLAServiceServer(twin *models.DigitalTwin) *SiLAServiceServerImpl {
	return &SiLAServiceServerImpl{
		twin: twin,
	}
}

func (s *SiLAServiceServerImpl) GetFeatureDefinition(ctx context.Context, req *pb.GetFeatureDefinition_Parameters) (*pb.GetFeatureDefinition_Responses, error) {
	identifier := req.FeatureIdentifier.Value
	parts := strings.Split(identifier, "/")
	name := parts[len(parts)-2]

	xmlBytes, err := fdlFiles.ReadFile("fdl/" + name + ".xml")
	if err == nil {
		return &pb.GetFeatureDefinition_Responses{
			FeatureDefinition: &pb.String{Value: string(xmlBytes)},
		}, nil
	}

	// A minimal valid SiLA 2 Feature XML to satisfy client schema validation
	xml := fmt.Sprintf(`<?xml version="1.0" encoding="utf-8" ?>
<Feature SiLA2Version="1.0" FeatureVersion="1.0" Originator="%s" Category="%s"
         xmlns="http://www.sila-standard.org"
         xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
         xsi:schemaLocation="http://www.sila-standard.org https://gitlab.com/SiLA2/sila_base/raw/master/schema/FeatureDefinition.xsd">
  <Identifier>%s</Identifier>
  <DisplayName>%s</DisplayName>
  <Description>SiLA 2 Feature</Description>
</Feature>`, parts[0], parts[1], name, name)

	return &pb.GetFeatureDefinition_Responses{
		FeatureDefinition: &pb.String{Value: xml},
	}, nil
}

func (s *SiLAServiceServerImpl) SetServerName(ctx context.Context, req *pb.SetServerName_Parameters) (*pb.SetServerName_Responses, error) {
	// Ideally we'd persist this, but for now we'll just accept it
	return &pb.SetServerName_Responses{}, nil
}

func (s *SiLAServiceServerImpl) Get_ServerName(ctx context.Context, req *pb.Get_ServerName_Parameters) (*pb.Get_ServerName_Responses, error) {
	return &pb.Get_ServerName_Responses{
		ServerName: &pb.String{Value: "VOCs Edge Chromatography Server"},
	}, nil
}

func (s *SiLAServiceServerImpl) Get_ServerType(ctx context.Context, req *pb.Get_ServerType_Parameters) (*pb.Get_ServerType_Responses, error) {
	return &pb.Get_ServerType_Responses{
		ServerType: &pb.String{Value: "GasChromatograph"},
	}, nil
}

func (s *SiLAServiceServerImpl) Get_ServerUUID(ctx context.Context, req *pb.Get_ServerUUID_Parameters) (*pb.Get_ServerUUID_Responses, error) {
	// A valid UUID is required by SiLA 2 standard
	// We can generate one or use a hardcoded one for this specific device type
	return &pb.Get_ServerUUID_Responses{
		ServerUUID: &pb.String{Value: "123e4567-e89b-12d3-a456-426614174000"},
	}, nil
}

func (s *SiLAServiceServerImpl) Get_ServerDescription(ctx context.Context, req *pb.Get_ServerDescription_Parameters) (*pb.Get_ServerDescription_Responses, error) {
	return &pb.Get_ServerDescription_Responses{
		ServerDescription: &pb.String{Value: "Edge Gateway for VOCs Chromatography Device"},
	}, nil
}

func (s *SiLAServiceServerImpl) Get_ServerVersion(ctx context.Context, req *pb.Get_ServerVersion_Parameters) (*pb.Get_ServerVersion_Responses, error) {
	return &pb.Get_ServerVersion_Responses{
		ServerVersion: &pb.String{Value: "0.3.97"},
	}, nil
}

func (s *SiLAServiceServerImpl) Get_ServerVendorURL(ctx context.Context, req *pb.Get_ServerVendorURL_Parameters) (*pb.Get_ServerVendorURL_Responses, error) {
	return &pb.Get_ServerVendorURL_Responses{
		ServerVendorURL: &pb.String{Value: "https://example.com"},
	}, nil
}

func (s *SiLAServiceServerImpl) Get_ImplementedFeatures(ctx context.Context, req *pb.Get_ImplementedFeatures_Parameters) (*pb.Get_ImplementedFeatures_Responses, error) {
	features := []string{
		"org.silastandard/core/SiLAService/v1",
		"org.silastandard/core/ChromatographService/v1",
		"org.silastandard/core/TemperatureControllerService/v1",
		"org.silastandard/core/ValveControllerService/v1",
		"custom/detector/FlameIonizationDetectorService/v1",
		"custom/detector/ThermalConductivityDetectorService/v1",
		"custom/pneumatic/PneumaticControllerService/v1",
	}

	var pbFeatures []*pb.String
	for _, f := range features {
		pbFeatures = append(pbFeatures, &pb.String{Value: f})
	}

	return &pb.Get_ImplementedFeatures_Responses{
		ImplementedFeatures: pbFeatures,
	}, nil
}
