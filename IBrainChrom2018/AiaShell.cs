using System;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class AiaShell : AIA
{
	public DataArr actual_delay_time = new DataArr();

	public DataArr actual_run_time_length = new DataArr();

	public DataArr actual_sampling_interval = new DataArr();

	public string administrative_comments = "none for now";

	public string aia_template_revision = "1.0";

	public string autosampler_position = "1.56";

	public DataArr baseline_start_time = new DataArr();

	public DataArr baseline_start_value = new DataArr();

	public DataArr baseline_stop_time = new DataArr();

	public DataArr baseline_stop_value = new DataArr();

	public string company_method_id = "SAXYZ";

	public string company_method_name = "sandope analysis XYZ";

	public string dataset_completeness = "C1+C2";

	public string dataset_date_time_stamp = "2005,08,22,13:51";

	public string dataset_origin = "Dalian Elite company";

	public string dataset_owner = "China companies";

	public string detecter_unit = Class49.MesureUnit();

	public string detection_method_comments = "An UV detecter";

	public string detection_method_name = "default.mth";

	public string detection_method_table_name = "test 1";

	public DataArr detector_maximum_value = new DataArr();

	public DataArr detector_minimum_value = new DataArr();

	public string detector_name = "UV 254 nm";

	public DetectorStyle detectorStyle;

	public DataArr error_log = new DataArr();

	public uint error_number;

	public string experiment_title = "EChrom---REPORT";

	public string file_name = "";

	public string injection_date_time_stamp = "20021024101010-0001";

	public string languages = "English - only for now";

	public DataArr manually_reintegrated_peaks = new DataArr();

	public DataArr mass_on_column = new DataArr();

	public DataArr migration_time = new DataArr();

	public string netcdf_revision = "2.0";

	public string operator_name = "Administrator";

	public DataArr ordinate_times = new DataArr();

	public DataArr ordinate_values = new DataArr();

	public DataArr peak_amount = new DataArr();

	public string peak_amount_unit = "%";

	public DataArr peak_area = new DataArr();

	public DataArr peak_area_percent = new DataArr();

	public DataArr peak_area_square_root = new DataArr();

	public DataArr peak_asymmetry = new DataArr();

	public DataArr peak_efficiency = new DataArr();

	public DataArr peak_end_time = new DataArr();

	public DataArr peak_height = new DataArr();

	public DataArr peak_height_percent = new DataArr();

	public DataArr peak_name = new DataArr();

	public uint peak_number;

	public string peak_processing_date_time_stamp = "20021024101010-0500";

	public string peak_processing_method_name = "test processing method";

	public string peak_processing_results_comments = "Level 1 calibration results";

	public string peak_processing_results_table_name = "Imp1. Guide Demo Code";

	public DataArr peak_retention_time = new DataArr();

	public DataArr peak_start_detection_code = new DataArr();

	public DataArr peak_start_time = new DataArr();

	public DataArr peak_stop_detection_code = new DataArr();

	public DataArr peak_width = new DataArr();

	public uint point_number;

	public string post_experiment_program_name = "response calibration";

	public string pre_experiment_program_name = "setup";

	public string raw_data_table_name = "test raw data set";

	public DataArr retention_index = new DataArr();

	public string retention_unit = "time in minutes";

	public string sample_amount = "2.0";

	public string sample_cali_stand = "N";

	public string sample_dilution = "1.0";

	public string sample_gpc_alpha = "14.1";

	public string sample_gpc_k = "14.1";

	public string sample_id = "JOU812";

	public string sample_id_comments = "none";

	public string sample_injection_volume = "10 ul";

	public string sample_istd_amount = "0.1";

	public string sample_name = "test sample中国";

	public string sample_type = "control";

	public string separation_experiment_type = "liquid chromatography";

	public string source_file_reference = "IODINE::dka10:[aia]test.cdf";

	public string uniform_sampling_flag = "Y";

	public void data_AIA(AccStyle accStyle)
	{
		switch (accStyle)
		{
		case AccStyle.Write:
			switch (detectorStyle)
			{
			case DetectorStyle.General:
			{
				for (int l = 0; l < dimArr.dims.Length; l++)
				{
					string text6 = dimArr.dims[l].name.ToString();
					if (text6 != null)
					{
						switch (text6)
						{
						case "peak_number":
							dimArr.dims[l].dimLength = peak_number;
							break;
						case "error_number":
							dimArr.dims[l].dimLength = error_number;
							break;
						case "point_number":
							dimArr.dims[l].dimLength = point_number;
							break;
						}
					}
				}
				for (int m = 0; m < gAttrArr.attrs.Length; m++)
				{
					string text7 = gAttrArr.attrs[m].name.ToString();
					string text8;
					switch (text8 = text7)
					{
					case "dataset_completeness":
						gAttrArr.attrs[m].data.chars = dataset_completeness.ToCharArray();
						break;
					case "aia_template_revision":
						gAttrArr.attrs[m].data.chars = aia_template_revision.ToCharArray();
						break;
					case "netcdf_revision":
						gAttrArr.attrs[m].data.chars = netcdf_revision.ToCharArray();
						break;
					case "languages":
						gAttrArr.attrs[m].data.chars = languages.ToCharArray();
						break;
					case "administrative_comments":
						gAttrArr.attrs[m].data.chars = administrative_comments.ToCharArray();
						break;
					case "dataset_origin":
						gAttrArr.attrs[m].data.chars = dataset_origin.ToCharArray();
						break;
					case "dataset_owner":
						gAttrArr.attrs[m].data.chars = dataset_owner.ToCharArray();
						break;
					case "dataset_date_time_stamp":
						gAttrArr.attrs[m].data.chars = dataset_date_time_stamp.ToCharArray();
						break;
					case "injection_date_time_stamp":
						gAttrArr.attrs[m].data.chars = injection_date_time_stamp.ToCharArray();
						break;
					case "experiment_title":
						gAttrArr.attrs[m].data.chars = experiment_title.ToCharArray();
						break;
					case "operator_name":
						gAttrArr.attrs[m].data.chars = operator_name.ToCharArray();
						break;
					case "separation_experiment_type":
						gAttrArr.attrs[m].data.chars = separation_experiment_type.ToCharArray();
						break;
					case "company_method_name":
						gAttrArr.attrs[m].data.chars = company_method_name.ToCharArray();
						break;
					case "company_method_id":
						gAttrArr.attrs[m].data.chars = company_method_id.ToCharArray();
						break;
					case "pre_experiment_program_name":
						gAttrArr.attrs[m].data.chars = pre_experiment_program_name.ToCharArray();
						break;
					case "post_experiment_program_name":
						gAttrArr.attrs[m].data.chars = post_experiment_program_name.ToCharArray();
						break;
					case "source_file_reference":
						gAttrArr.attrs[m].data.chars = source_file_reference.ToCharArray();
						break;
					case "sample_id_comments":
						gAttrArr.attrs[m].data.chars = sample_id_comments.ToCharArray();
						break;
					case "sample_id":
						gAttrArr.attrs[m].data.chars = sample_id.ToCharArray();
						break;
					case "sample_name":
						gAttrArr.attrs[m].data.chars = sample_name.ToCharArray();
						break;
					case "sample_type":
						gAttrArr.attrs[m].data.chars = sample_type.ToCharArray();
						break;
					case "sample_injection_volume":
						gAttrArr.attrs[m].data.chars = sample_injection_volume.ToCharArray();
						break;
					case "sample_amount":
						gAttrArr.attrs[m].data.chars = sample_amount.ToCharArray();
						break;
					case "detection_method_table_name":
						gAttrArr.attrs[m].data.chars = detection_method_table_name.ToCharArray();
						break;
					case "detection_method_comments":
						gAttrArr.attrs[m].data.chars = detection_method_comments.ToCharArray();
						break;
					case "detection_method_name":
						gAttrArr.attrs[m].data.chars = detection_method_name.ToCharArray();
						break;
					case "detector_name":
						gAttrArr.attrs[m].data.chars = detector_name.ToCharArray();
						break;
					case "detecter_unit":
						gAttrArr.attrs[m].data.chars = detecter_unit.ToCharArray();
						break;
					case "raw_data_table_name":
						gAttrArr.attrs[m].data.chars = raw_data_table_name.ToCharArray();
						break;
					case "retention_unit":
						gAttrArr.attrs[m].data.chars = retention_unit.ToCharArray();
						break;
					case "peak_processing_results_table_name":
						gAttrArr.attrs[m].data.chars = peak_processing_results_table_name.ToCharArray();
						break;
					case "peak_processing_results_comments":
						gAttrArr.attrs[m].data.chars = peak_processing_results_comments.ToCharArray();
						break;
					case "peak_processing_method_name":
						gAttrArr.attrs[m].data.chars = peak_processing_method_name.ToCharArray();
						break;
					case "peak_processing_date_time_stamp":
						gAttrArr.attrs[m].data.chars = peak_processing_date_time_stamp.ToCharArray();
						break;
					case "peak_amount_unit":
						gAttrArr.attrs[m].data.chars = peak_amount_unit.ToCharArray();
						break;
					case "sample_istd_amount":
						gAttrArr.attrs[m].data.chars = sample_istd_amount.ToCharArray();
						break;
					case "sample_dilution":
						gAttrArr.attrs[m].data.chars = sample_dilution.ToCharArray();
						break;
					case "sample_cali_stand":
						gAttrArr.attrs[m].data.chars = sample_cali_stand.ToCharArray();
						break;
					case "sample_gpc_k":
						gAttrArr.attrs[m].data.chars = sample_gpc_k.ToCharArray();
						break;
					case "sample_gpc_alpha":
						gAttrArr.attrs[m].data.chars = sample_gpc_alpha.ToCharArray();
						break;
					case "file_name":
						gAttrArr.attrs[m].data.chars = file_name.ToCharArray();
						break;
					}
				}
				for (int n = 0; n < varArr.vars.Length; n++)
				{
					string text9 = new string(varArr.vars[n].name);
					string text10;
					switch (text10 = text9)
					{
					case "error_log":
						varArr.vars[n].data.LoadFromObject(error_log);
						break;
					case "detector_maximum_value":
						varArr.vars[n].data.LoadFromObject(detector_maximum_value);
						break;
					case "detector_minimum_value":
						varArr.vars[n].data.LoadFromObject(detector_minimum_value);
						break;
					case "actual_run_time_length":
						varArr.vars[n].data.LoadFromObject(actual_run_time_length);
						break;
					case "actual_sampling_interval":
						varArr.vars[n].data.LoadFromObject(actual_sampling_interval);
						break;
					case "actual_delay_time":
						varArr.vars[n].data.LoadFromObject(actual_delay_time);
						break;
					case "ordinate_values":
					{
						int num2 = 0;
						for (; n < varArr.vars[n].svAttr.attrs.Length; n++)
						{
							text9 = new string(varArr.vars[n].svAttr.attrs[num2].name);
							if (text9 == null)
							{
								continue;
							}
							if (!(text9 == "uniform_sampling_flag"))
							{
								if (text9 == "autosampler_position")
								{
									varArr.vars[n].svAttr.attrs[num2].data.chars = autosampler_position.ToCharArray();
								}
							}
							else
							{
								varArr.vars[n].svAttr.attrs[num2].data.chars = uniform_sampling_flag.ToCharArray();
							}
						}
						varArr.vars[n].data.LoadFromObject(ordinate_values);
						break;
					}
					case "baseline_start_time":
						varArr.vars[n].data.LoadFromObject(baseline_start_time);
						break;
					case "baseline_start_value":
						varArr.vars[n].data.LoadFromObject(baseline_start_value);
						break;
					case "baseline_stop_time":
						varArr.vars[n].data.LoadFromObject(baseline_stop_time);
						break;
					case "baseline_stop_value":
						varArr.vars[n].data.LoadFromObject(baseline_stop_value);
						break;
					case "peak_start_detection_code":
						varArr.vars[n].data.LoadFromObject(peak_start_detection_code);
						break;
					case "peak_stop_detection_code":
						varArr.vars[n].data.LoadFromObject(peak_stop_detection_code);
						break;
					case "peak_retention_time":
						varArr.vars[n].data.LoadFromObject(peak_retention_time);
						break;
					case "peak_name":
						varArr.vars[n].data.LoadFromObject(peak_name);
						break;
					case "peak_amount":
						varArr.vars[n].data.LoadFromObject(peak_amount);
						break;
					case "peak_start_time":
						varArr.vars[n].data.LoadFromObject(peak_start_time);
						break;
					case "peak_end_time":
						varArr.vars[n].data.LoadFromObject(peak_end_time);
						break;
					case "peak_width":
						varArr.vars[n].data.LoadFromObject(peak_width);
						break;
					case "peak_area":
						varArr.vars[n].data.LoadFromObject(peak_area);
						break;
					case "peak_area_percent":
						varArr.vars[n].data.LoadFromObject(peak_area_percent);
						break;
					case "peak_area_square_root":
						varArr.vars[n].data.LoadFromObject(peak_area_square_root);
						break;
					case "peak_height":
						varArr.vars[n].data.LoadFromObject(peak_height);
						break;
					case "peak_height_percent":
						varArr.vars[n].data.LoadFromObject(peak_height_percent);
						break;
					case "peak_asymmetry":
						varArr.vars[n].data.LoadFromObject(peak_asymmetry);
						break;
					case "peak_efficiency":
						varArr.vars[n].data.LoadFromObject(peak_efficiency);
						break;
					case "manually_reintegrated_peaks":
						varArr.vars[n].data.LoadFromObject(manually_reintegrated_peaks);
						break;
					case "retention_index":
						varArr.vars[n].data.LoadFromObject(retention_index);
						break;
					case "migration_time":
						varArr.vars[n].data.LoadFromObject(migration_time);
						break;
					case "mass_on_column":
						varArr.vars[n].data.LoadFromObject(mass_on_column);
						break;
					case "ordinate_times":
						varArr.vars[n].data.LoadFromObject(ordinate_times);
						break;
					}
				}
				break;
			}
			}
			break;
		case AccStyle.Read:
			switch (detectorStyle)
			{
			case DetectorStyle.General:
			{
				for (int i = 0; i < dimArr.dims.Length; i++)
				{
					string text = dimArr.dims[i].name.ToString();
					if (text != null)
					{
						switch (text)
						{
						case "peak_number":
							peak_number = dimArr.dims[i].dimLength;
							break;
						case "error_number":
							error_number = dimArr.dims[i].dimLength;
							break;
						case "point_number":
							point_number = dimArr.dims[i].dimLength;
							break;
						}
					}
				}
				for (int j = 0; j < gAttrArr.attrs.Length; j++)
				{
					string text2 = new string(gAttrArr.attrs[j].name);
					string text3;
					switch (text3 = text2)
					{
					case "dataset_completeness":
						dataset_completeness = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "aia_template_revision":
						aia_template_revision = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "netcdf_revision":
						netcdf_revision = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "languages":
						languages = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "administrative_comments":
						administrative_comments = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "dataset_origin":
						dataset_origin = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "dataset_owner":
						dataset_owner = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "dataset_date_time_stamp":
						dataset_date_time_stamp = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "injection_date_time_stamp":
						injection_date_time_stamp = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "experiment_title":
						experiment_title = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "operator_name":
						operator_name = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "separation_experiment_type":
						separation_experiment_type = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "company_method_name":
						company_method_name = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "company_method_id":
						company_method_id = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "pre_experiment_program_name":
						pre_experiment_program_name = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "post_experiment_program_name":
						post_experiment_program_name = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "source_file_reference":
						source_file_reference = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "sample_id_comments":
						sample_id_comments = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "sample_id":
						sample_id = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "sample_name":
						sample_name = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "sample_type":
						sample_type = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "sample_injection_volume":
						sample_injection_volume = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "sample_amount":
						sample_amount = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "detection_method_table_name":
						detection_method_table_name = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "detection_method_comments":
						detection_method_comments = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "detection_method_name":
						detection_method_name = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "detector_name":
						detector_name = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "detecter_unit":
						detecter_unit = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "raw_data_table_name":
						raw_data_table_name = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "retention_unit":
						retention_unit = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "peak_processing_results_table_name":
						peak_processing_results_table_name = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "peak_processing_results_comments":
						peak_processing_results_comments = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "peak_processing_method_name":
						peak_processing_method_name = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "peak_processing_date_time_stamp":
						peak_processing_date_time_stamp = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "peak_amount_unit":
						peak_amount_unit = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "sample_istd_amount":
						sample_istd_amount = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "sample_dilution":
						sample_dilution = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "sample_cali_stand":
						sample_cali_stand = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "sample_gpc_k":
						sample_gpc_k = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "sample_gpc_alpha":
						sample_gpc_alpha = new string(gAttrArr.attrs[j].data.chars);
						break;
					case "file_name":
						file_name = new string(gAttrArr.attrs[j].data.chars);
						break;
					}
				}
				for (int k = 0; k < varArr.vars.Length; k++)
				{
					string text4 = new string(varArr.vars[k].name);
					string text5;
					switch (text5 = text4)
					{
					case "error_log":
						error_log.LoadFromObject(varArr.vars[k].data);
						break;
					case "detector_maximum_value":
						detector_maximum_value.LoadFromObject(varArr.vars[k].data);
						break;
					case "detector_minimum_value":
						detector_minimum_value.LoadFromObject(varArr.vars[k].data);
						break;
					case "actual_run_time_length":
						actual_run_time_length.LoadFromObject(varArr.vars[k].data);
						break;
					case "actual_sampling_interval":
						actual_sampling_interval.LoadFromObject(varArr.vars[k].data);
						break;
					case "actual_delay_time":
						actual_delay_time.LoadFromObject(varArr.vars[k].data);
						break;
					case "ordinate_values":
					{
						int num = 0;
						for (; k < varArr.vars[k].svAttr.attrs.Length; k++)
						{
							text4 = new string(varArr.vars[k].svAttr.attrs[num].name);
							if (text4 == null)
							{
								continue;
							}
							if (!(text4 == "uniform_sampling_flag"))
							{
								if (text4 == "autosampler_position")
								{
									autosampler_position = varArr.vars[k].svAttr.attrs[num].data.chars.ToString();
								}
							}
							else
							{
								uniform_sampling_flag = varArr.vars[k].svAttr.attrs[num].data.chars.ToString();
							}
						}
						ordinate_values.LoadFromObject(varArr.vars[k].data);
						break;
					}
					case "baseline_start_time":
						baseline_start_time.LoadFromObject(varArr.vars[k].data);
						break;
					case "baseline_start_value":
						baseline_start_value.LoadFromObject(varArr.vars[k].data);
						break;
					case "baseline_stop_time":
						baseline_stop_time.LoadFromObject(varArr.vars[k].data);
						break;
					case "baseline_stop_value":
						baseline_stop_value.LoadFromObject(varArr.vars[k].data);
						break;
					case "peak_start_detection_code":
						peak_start_detection_code.LoadFromObject(varArr.vars[k].data);
						break;
					case "peak_stop_detection_code":
						peak_stop_detection_code.LoadFromObject(varArr.vars[k].data);
						break;
					case "peak_retention_time":
						peak_retention_time.LoadFromObject(varArr.vars[k].data);
						break;
					case "peak_name":
						peak_name.LoadFromObject(varArr.vars[k].data);
						break;
					case "peak_amount":
						peak_amount.LoadFromObject(varArr.vars[k].data);
						break;
					case "peak_start_time":
						peak_start_time.LoadFromObject(varArr.vars[k].data);
						break;
					case "peak_end_time":
						peak_end_time.LoadFromObject(varArr.vars[k].data);
						break;
					case "peak_width":
						peak_width.LoadFromObject(varArr.vars[k].data);
						break;
					case "peak_area":
						peak_area.LoadFromObject(varArr.vars[k].data);
						break;
					case "peak_area_percent":
						peak_area_percent.LoadFromObject(varArr.vars[k].data);
						break;
					case "peak_area_square_root":
						peak_area_square_root.LoadFromObject(varArr.vars[k].data);
						break;
					case "peak_height":
						peak_height.LoadFromObject(varArr.vars[k].data);
						break;
					case "peak_height_percent":
						peak_height_percent.LoadFromObject(varArr.vars[k].data);
						break;
					case "peak_asymmetry":
						peak_asymmetry.LoadFromObject(varArr.vars[k].data);
						break;
					case "peak_efficiency":
						peak_efficiency.LoadFromObject(varArr.vars[k].data);
						break;
					case "manually_reintegrated_peaks":
						manually_reintegrated_peaks.LoadFromObject(varArr.vars[k].data);
						break;
					case "retention_index":
						retention_index.LoadFromObject(varArr.vars[k].data);
						break;
					case "migration_time":
						migration_time.LoadFromObject(varArr.vars[k].data);
						break;
					case "mass_on_column":
						mass_on_column.LoadFromObject(varArr.vars[k].data);
						break;
					case "ordinate_times":
						ordinate_times.LoadFromObject(varArr.vars[k].data);
						break;
					}
				}
				break;
			}
			}
			break;
		}
	}

	public void PrepareAIA()
	{
		DetectorStyle detectorStyle = this.detectorStyle;
		if (detectorStyle == DetectorStyle.General && Class49.smethod_36() == 1)
		{
			dimArr.Clear();
			dimArr.AddDim("point_number");
			dimArr.AddDim("peak_number");
			dimArr.AddDim("error_number");
			gAttrArr.Clear();
			if (dataset_completeness != "")
			{
				gAttrArr.AddAttr("dataset_completeness");
			}
			if (aia_template_revision != "")
			{
				gAttrArr.AddAttr("aia_template_revision");
			}
			if (netcdf_revision != "")
			{
				gAttrArr.AddAttr("netcdf_revision");
			}
			if (languages != "")
			{
				gAttrArr.AddAttr("languages");
			}
			if (administrative_comments != "")
			{
				gAttrArr.AddAttr("administrative_comments");
			}
			if (dataset_origin != "")
			{
				gAttrArr.AddAttr("dataset_origin");
			}
			if (dataset_owner != "")
			{
				gAttrArr.AddAttr("dataset_owner");
			}
			if (dataset_date_time_stamp != "")
			{
				gAttrArr.AddAttr("dataset_date_time_stamp");
			}
			if (injection_date_time_stamp != "")
			{
				gAttrArr.AddAttr("injection_date_time_stamp");
			}
			if (experiment_title != "")
			{
				gAttrArr.AddAttr("experiment_title");
			}
			if (operator_name != "")
			{
				gAttrArr.AddAttr("operator_name");
			}
			if (separation_experiment_type != "")
			{
				gAttrArr.AddAttr("separation_experiment_type");
			}
			if (company_method_name != "")
			{
				gAttrArr.AddAttr("company_method_name");
			}
			if (company_method_id != "")
			{
				gAttrArr.AddAttr("company_method_id");
			}
			if (pre_experiment_program_name != "")
			{
				gAttrArr.AddAttr("pre_experiment_program_name");
			}
			if (post_experiment_program_name != "")
			{
				gAttrArr.AddAttr("post_experiment_program_name");
			}
			if (source_file_reference != "")
			{
				gAttrArr.AddAttr("source_file_reference");
			}
			if (sample_id_comments != "")
			{
				gAttrArr.AddAttr("sample_id_comments");
			}
			if (sample_id != "")
			{
				gAttrArr.AddAttr("sample_id");
			}
			if (sample_name != "")
			{
				gAttrArr.AddAttr("sample_name");
			}
			if (sample_type != "")
			{
				gAttrArr.AddAttr("sample_type");
			}
			if (sample_injection_volume != "")
			{
				gAttrArr.AddAttr("sample_injection_volume");
			}
			if (sample_amount != "")
			{
				gAttrArr.AddAttr("sample_amount");
			}
			if (detection_method_table_name != "")
			{
				gAttrArr.AddAttr("detection_method_table_name");
			}
			if (detection_method_comments != "")
			{
				gAttrArr.AddAttr("detection_method_comments");
			}
			if (detection_method_name != "")
			{
				gAttrArr.AddAttr("detection_method_name");
			}
			if (detector_name != "")
			{
				gAttrArr.AddAttr("detector_name");
			}
			if (detecter_unit != "")
			{
				gAttrArr.AddAttr("detecter_unit");
			}
			if (raw_data_table_name != "")
			{
				gAttrArr.AddAttr("raw_data_table_name");
			}
			if (retention_unit != "")
			{
				gAttrArr.AddAttr("retention_unit");
			}
			if (peak_processing_results_table_name != "")
			{
				gAttrArr.AddAttr("peak_processing_results_table_name");
			}
			if (peak_processing_results_comments != "")
			{
				gAttrArr.AddAttr("peak_processing_results_comments");
			}
			if (peak_processing_method_name != "")
			{
				gAttrArr.AddAttr("peak_processing_method_name");
			}
			if (peak_processing_date_time_stamp != "")
			{
				gAttrArr.AddAttr("peak_processing_date_time_stamp");
			}
			if (peak_amount_unit != "")
			{
				gAttrArr.AddAttr("peak_amount_unit");
			}
			if (sample_istd_amount != "")
			{
				gAttrArr.AddAttr("sample_istd_amount");
			}
			if (sample_dilution != "")
			{
				gAttrArr.AddAttr("sample_dilution");
			}
			if (sample_cali_stand != "")
			{
				gAttrArr.AddAttr("sample_cali_stand");
			}
			if (sample_gpc_k != "")
			{
				gAttrArr.AddAttr("sample_gpc_k");
			}
			if (sample_gpc_alpha != "")
			{
				gAttrArr.AddAttr("sample_gpc_alpha");
			}
			if (file_name != "")
			{
				gAttrArr.AddAttr("file_name");
			}
			varArr.Clear();
			if (error_log.ElemsNum != 0)
			{
				varArr.AddVar("error_log", dimArr, new string[1] { "error_number" }, null);
			}
			if (detector_maximum_value.ElemsNum != 0)
			{
				varArr.AddVar("detector_maximum_value", dimArr, null, null);
			}
			if (detector_minimum_value.ElemsNum != 0)
			{
				varArr.AddVar("detector_minimum_value", dimArr, null, null);
			}
			if (actual_run_time_length.ElemsNum != 0)
			{
				varArr.AddVar("actual_run_time_length", dimArr, null, null);
			}
			if (actual_sampling_interval.ElemsNum != 0)
			{
				varArr.AddVar("actual_sampling_interval", dimArr, null, null);
			}
			if (actual_delay_time.ElemsNum != 0)
			{
				varArr.AddVar("actual_delay_time", dimArr, null, null);
			}
			if (ordinate_values.ElemsNum != 0)
			{
				varArr.AddVar("ordinate_values", dimArr, new string[1] { "point_number" }, new string[2] { "uniform_sampling_flag", "autosampler_position" });
				varArr.AddVar("ordinate_times", dimArr, null, null);
			}
			if (baseline_start_time.ElemsNum != 0)
			{
				varArr.AddVar("baseline_start_time", dimArr, new string[1] { "peak_number" }, null);
			}
			if (baseline_start_value.ElemsNum != 0)
			{
				varArr.AddVar("baseline_start_value", dimArr, new string[1] { "peak_number" }, null);
			}
			if (baseline_stop_time.ElemsNum != 0)
			{
				varArr.AddVar("baseline_stop_time", dimArr, new string[1] { "peak_number" }, null);
			}
			if (baseline_stop_value.ElemsNum != 0)
			{
				varArr.AddVar("baseline_stop_value", dimArr, new string[1] { "peak_number" }, null);
			}
			if (peak_start_detection_code.ElemsNum != 0)
			{
				varArr.AddVar("peak_start_detection_code", dimArr, new string[1] { "peak_number" }, null);
			}
			if (peak_stop_detection_code.ElemsNum != 0)
			{
				varArr.AddVar("peak_stop_detection_code", dimArr, new string[1] { "peak_number" }, null);
			}
			if (peak_retention_time.ElemsNum != 0)
			{
				varArr.AddVar("peak_retention_time", dimArr, new string[1] { "peak_number" }, null);
			}
			if (peak_name.ElemsNum != 0)
			{
				varArr.AddVar("peak_name", dimArr, new string[1] { "peak_number" }, null);
			}
			if (peak_amount.ElemsNum != 0)
			{
				varArr.AddVar("peak_amount", dimArr, new string[1] { "peak_number" }, null);
			}
			if (peak_start_time.ElemsNum != 0)
			{
				varArr.AddVar("peak_start_time", dimArr, new string[1] { "peak_number" }, null);
			}
			if (peak_end_time.ElemsNum != 0)
			{
				varArr.AddVar("peak_end_time", dimArr, new string[1] { "peak_number" }, null);
			}
			if (peak_width.ElemsNum != 0)
			{
				varArr.AddVar("peak_width", dimArr, new string[1] { "peak_number" }, null);
			}
			if (peak_area.ElemsNum != 0)
			{
				varArr.AddVar("peak_area", dimArr, new string[1] { "peak_number" }, null);
			}
			if (peak_area_percent.ElemsNum != 0)
			{
				varArr.AddVar("peak_area_percent", dimArr, new string[1] { "peak_number" }, null);
			}
			if (peak_area_square_root.ElemsNum != 0)
			{
				varArr.AddVar("peak_area_square_root", dimArr, null, null);
			}
			if (peak_height.ElemsNum != 0)
			{
				varArr.AddVar("peak_height", dimArr, new string[1] { "peak_number" }, null);
			}
			if (peak_height_percent.ElemsNum != 0)
			{
				varArr.AddVar("peak_height_percent", dimArr, new string[1] { "peak_number" }, null);
			}
			if (peak_asymmetry.ElemsNum != 0)
			{
				varArr.AddVar("peak_asymmetry", dimArr, new string[1] { "peak_number" }, null);
			}
			if (peak_efficiency.ElemsNum != 0)
			{
				varArr.AddVar("peak_efficiency", dimArr, new string[1] { "peak_number" }, null);
			}
			if (manually_reintegrated_peaks.ElemsNum != 0)
			{
				varArr.AddVar("manually_reintegrated_peaks", dimArr, new string[1] { "peak_number" }, null);
			}
			if (retention_index.ElemsNum != 0)
			{
				varArr.AddVar("retention_index", dimArr, new string[1] { "peak_number" }, null);
			}
			if (migration_time.ElemsNum != 0)
			{
				varArr.AddVar("migration_time", dimArr, new string[1] { "peak_number" }, null);
			}
			if (mass_on_column.ElemsNum != 0)
			{
				varArr.AddVar("mass_on_column", dimArr, new string[1] { "peak_number" }, null);
			}
		}
	}

	public void ResetInformations()
	{
		point_number = 0u;
		peak_number = 0u;
		dataset_completeness = "";
		aia_template_revision = AIA.version + ".0";
		netcdf_revision = "2.0";
		languages = "English";
		administrative_comments = "";
		dataset_origin = "";
		dataset_owner = "";
		dataset_date_time_stamp = DateTime.Now.ToString();
		injection_date_time_stamp = "";
		experiment_title = "";
		operator_name = "";
		separation_experiment_type = "";
		company_method_name = "";
		company_method_id = "";
		pre_experiment_program_name = "";
		post_experiment_program_name = "";
		source_file_reference = "";
		sample_id_comments = "";
		sample_id = "";
		sample_name = "";
		sample_type = "";
		sample_injection_volume = "";
		sample_amount = "";
		detection_method_table_name = "";
		detection_method_comments = "";
		detection_method_name = "";
		detector_name = "";
		detecter_unit = "";
		raw_data_table_name = "";
		retention_unit = "";
		peak_processing_results_table_name = "";
		peak_processing_results_comments = "";
		peak_processing_method_name = "";
		peak_processing_date_time_stamp = "";
		peak_amount_unit = "";
		sample_istd_amount = "";
		sample_dilution = "";
		sample_cali_stand = "";
		sample_gpc_k = "";
		sample_gpc_alpha = "";
		file_name = "";
		error_log.Clear();
		detector_maximum_value.Clear();
		detector_minimum_value.Clear();
		actual_run_time_length.Clear();
		actual_sampling_interval.Clear();
		actual_delay_time.Clear();
		ordinate_values.Clear();
		uniform_sampling_flag = "Y";
		autosampler_position = "1.56";
		baseline_start_time.Clear();
		baseline_start_value.Clear();
		baseline_stop_time.Clear();
		baseline_stop_value.Clear();
		peak_start_detection_code.Clear();
		peak_stop_detection_code.Clear();
		peak_retention_time.Clear();
		peak_name.Clear();
		peak_amount.Clear();
		peak_start_time.Clear();
		peak_end_time.Clear();
		peak_width.Clear();
		peak_area.Clear();
		peak_area_percent.Clear();
		peak_area_square_root.Clear();
		peak_height.Clear();
		peak_height_percent.Clear();
		peak_asymmetry.Clear();
		peak_efficiency.Clear();
		manually_reintegrated_peaks.Clear();
		retention_index.Clear();
		migration_time.Clear();
		mass_on_column.Clear();
		ordinate_times.Clear();
	}
}
