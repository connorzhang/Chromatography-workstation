package models

// Peak 表示单次进样分析后，计算出的单峰结果
type Peak struct {
	ID         string  `json:"id"`
	RunID      string  `json:"run_id"`      // 关联的分析批次ID
	Name       string  `json:"name"`        // 组分名称 (如果识别出)
	RetainTime float64 `json:"retain_time"` // 保留时间 (min)
	Area       float64 `json:"area"`        // 峰面积
	Height     float64 `json:"height"`      // 峰高
	Width      float64 `json:"width"`       // 峰宽
	StartTime  float64 `json:"start_time"`  // 峰起点时间
	EndTime    float64 `json:"end_time"`    // 峰终点时间
	Amount     float64 `json:"amount"`      // 最终浓度/含量 (如 mg/m3)
	PeakStyle  int     `json:"peak_style"`  // 峰类型 (对应遗留代码的类型枚举，如基线峰、拖尾峰)
}

// Method 包含分析方法的核心配置
type Method struct {
	ID          string       `json:"id"`
	Name        string       `json:"name"`
	Compounds   []Compound   `json:"compounds"`   // 组分表
	Integration Integration  `json:"integration"` // 积分参数
}

// Compound 定义需要识别的物质及其校准参数
type Compound struct {
	ID          string  `json:"id"`
	MethodID    string  `json:"method_id"`
	Name        string  `json:"name"`         // 组分名称 (如 "甲烷", "总烃")
	RetainTime  float64 `json:"retain_time"`  // 标准保留时间
	LeftWindow  float64 `json:"left_window"`  // 左识别窗口 (默认绝对时间)
	RightWindow float64 `json:"right_window"` // 右识别窗口
	IsISTD      bool    `json:"is_istd"`      // 是否为内标物
	RespStyle   int     `json:"resp_style"`   // 响应类型 (0: 面积, 1: 峰高)
	CurveFunc   int     `json:"curve_func"`   // 拟合曲线函数 (0: 线性, 1: 多项式等)
	Levels      []Level `json:"levels"`       // 校准级别集合 (对应原 levels 数组)
}

// Level 用于多点标定，描述在某个标气浓度下，期望得到的面积/高度
type Level struct {
	LevelIndex int     `json:"level_index"` // 级别编号 (1-20)
	Amount     float64 `json:"amount"`      // 标准浓度 (如 10.0 mg/m3)
	Response   float64 `json:"response"`    // 对应的标准响应值 (面积或峰高)
}

// Integration 指导内核如何进行基线切割
type Integration struct {
	MinArea   float64 `json:"min_area"`   // 最小峰面积阈值 (过滤噪声)
	MinHeight float64 `json:"min_height"` // 最小峰高阈值
	Slope     float64 `json:"slope"`      // 斜率阈值 (识别峰起点/终点)
	MinWidth  float64 `json:"min_width"`  // 最小峰宽
}

// EventRow 外部事件与多位阀切换时间程序
type EventRow struct {
	Time      float64 `json:"time"`       // 触发时间 (min)
	EventMask int     `json:"event_mask"` // 事件掩码 (对应继电器/阀的状态)
}

// HardwareConfig 仪器控制参数，保存当前的控温、点火及气路设定值
type HardwareConfig struct {
	Temperatures map[string]float64 `json:"temperatures"` // key: "Inj1", "Col", "Det1", "Valve", "Inj2", "Det2", "Protect"
	TempEnables  map[string]bool    `json:"temp_enables"` // key: "Inj1", "Col", "Det1", "Inj2", "Det2", "Det3"
	EPCs         map[string]float64 `json:"epcs"`         // key: "Carrier1", "H2_1", "Air1", "Aux", "Carrier2", "H2_2", "Air2"
	Ignite       bool               `json:"ignite"`       // FID 点火状态
	IgniteThreshold1 float64        `json:"igniteThreshold1"`
	IgniteThreshold2 float64        `json:"igniteThreshold2"`
	IgniteDuration   float64        `json:"igniteDuration"`
	CycleInterval    float64        `json:"cycleInterval"`
	CycleCount       int            `json:"cycleCount"`
	Events       []EventRow         `json:"events"`       // 外部事件序列
	TCDBridgeCurrent uint8          `json:"tcdBridgeCurrent"` // TCD 桥流设定值
}

// UploadConfig 上传与数采仪配置
type UploadConfig struct {
	Ranges          map[string][]float64 `json:"ranges"`          // key: 组分名, value: [下限, 上限1, 上限2]
	Use420mA        bool                 `json:"use420mA"`        // 是否使用 4-20mA
	EnrichTemp      float64              `json:"enrichTemp"`      // 富集温度
	DesorbTemp      float64              `json:"desorbTemp"`      // 解析温度
	SampleFlow      float64              `json:"sampleFlow"`      // 样品流量
	EnrichTime      float64              `json:"enrichTime"`      // 富集时长
	DesorbTime      float64              `json:"desorbTime"`      // 解析时长
	DeviceNo        string               `json:"deviceNo"`        // 设备号
	UploadIP        string               `json:"uploadIP"`        // 上传IP
	UploadPort      int                  `json:"uploadPort"`      // 上传端口
	ChromatographIP string               `json:"chromatographIP"` // 色谱IP
	EnableUpload    bool                 `json:"enableUpload"`    // 是否启用上传
}

// ComponentCapability 定义标准硬件能力项 (符合 SiLA 2 Feature / OPC-UA LADS Component)
type ComponentCapability struct {
	ID       string  `json:"id"`                 // 内部标识, 如 "Col", "Event1", "TCD"
	Label    string  `json:"label"`              // 前端展示的中文名
	Type     string  `json:"type"`               // 类型: TemperatureZone, Valve, Detector, EPC
	MaxTemp  float64 `json:"max_temp,omitempty"` // 温度区最高限制
	MinTemp  float64 `json:"min_temp,omitempty"`
	MaxFlow  float64 `json:"max_flow,omitempty"`
	MaxPress float64 `json:"max_press,omitempty"`
}

// Capabilities 包含当前色谱仪支持的所有模块列表
type Capabilities struct {
	Temperatures []ComponentCapability `json:"temperatures"`
	Events       []ComponentCapability `json:"events"`
	Detectors    []ComponentCapability `json:"detectors"`
	EPCs         []ComponentCapability `json:"epcs"`
}

// SysConfig 系统高级配置 (如 MQTT，不依赖环境变量)
type SysConfig struct {
	MqttBroker       string `json:"mqtt_broker"`
	MqttTopic        string `json:"mqtt_topic"`
	MqttClientID     string `json:"mqtt_client_id"`
	MqttUser         string `json:"mqtt_user"`
	MqttPass         string `json:"mqtt_pass"`
	MqttEnabled      bool   `json:"mqtt_enabled"`
	MqttUploadInfo   bool   `json:"mqtt_upload_info"`   // 是否上传基础信息
	MqttUploadStatus bool   `json:"mqtt_upload_status"` // 是否上传实时状态
	MqttUploadResult bool   `json:"mqtt_upload_result"` // 是否上传分析结果
	MqttUploadLog    bool   `json:"mqtt_upload_log"`    // 是否上传系统日志
	MqttUploadDebug  bool   `json:"mqtt_upload_debug"`  // MQTT是否上传 DEBUG 级别日志
	AdminPass           string `json:"admin_pass"`            // 菜单加密密码
	DriverMode          string `json:"driver_mode"`           // 驱动模式: "legacy" 或 "modular"
	ModbusServerPort    int    `json:"modbus_server_port"`    // Modbus TCP Server 端口 (默认 1502)
	ModbusServerAddress string `json:"modbus_server_address"` // Modbus Server 设备地址标识
	ModbusUploadLog     bool   `json:"modbus_upload_log"`     // 是否将日志上传至 Modbus
	ModularDeviceID     string `json:"modular_device_id"`     // Modular架构: 自定义设备ID (默认: GC-MODULAR)
	ModularTCDPort      string `json:"modular_tcd_port"`      // Modular架构: TCD串口 (例: COM11)
	ModularTempPort     string `json:"modular_temp_port"`     // Modular架构: 温控板串口 (例: COM7)
	ModularTempSlaveID  int    `json:"modular_temp_slave_id"` // Modular架构: 温控板从机ID (默认 20)
	ModularEPCPort      string `json:"modular_epc_port"`      // Modular架构: EPC串口 (预留)
}
