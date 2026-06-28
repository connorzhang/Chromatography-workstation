package main

import (
"bytes"
"fmt"
"net/http"
)

func main() {
payload := []byte({"auth_pass":"123456","driver_mode":"modular","modular_tcd_port":"COM11","modular_temp_port":"COM7","modular_temp_slave_id":20})
resp, err := http.Post("http://10.8.5.23:8080/api/sysconfig", "application/json", bytes.NewBuffer(payload))
if err != nil {
fmt.Println(err)
return
}
defer resp.Body.Close()
buf := new(bytes.Buffer)
buf.ReadFrom(resp.Body)
fmt.Println(resp.StatusCode, buf.String())
}
