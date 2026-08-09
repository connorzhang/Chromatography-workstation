package main

import (
"encoding/json"
"fmt"
)

func main() {
payload := map[string]any{
"event": "测试中文",
"msg":   "这是中文乱码测试",
}
b, _ := json.Marshal(payload)
fmt.Println(string(b))
}
