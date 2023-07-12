package handlers

import (
	"github.com/gin-gonic/gin"
	"infra-api/internal/database"
	"infra-api/internal/models/dc"
	"infra-api/internal/services/powerdns"
	"infra-api/internal/transport"
	"log"
)

// GetVms Получить список всех виртуальных машин
func GetVms(c *gin.Context) {
	vms := database.GetVmsFromDb()
	c.JSON(200, vms)
}
func GetVmInfo(c *gin.Context) {
	vm := dc.Vm{}
	err := c.BindJSON(vm)
	if err != nil {
		c.JSON(400, transport.HTTPError{Type: "Bad request", Title: "Ошибка", Data: err.Error()})
		return
	}

	if vm.Id == 0 {
		c.JSON(400, transport.HTTPError{Type: "Bad request", Title: "Ошибка", Data: "Не передан корректный параметр Id в запросе"})
		return
	}

	vm, err = database.GetSingleVmFromDb(vm.Id)
	if err != nil {
		c.JSON(500, transport.HTTPError{Type: "Internal Server Error", Title: "Внутреняя ошибка сервера", Data: "Ошибка выполнения SQL команды"})
		return
	}

	if vm.Id == 0 {
		c.JSON(404, transport.HTTPError{Type: "Not Found", Title: "Предупреждение", Data: "Виртуальная машина не найдена"})
		return
	}

	c.JSON(200, vm)
}
func CreateVm(c *gin.Context) {
	vm := dc.Vm{}
	err := c.BindJSON(&vm)
	if err != nil {
		log.Println("ERROR:", err.Error())
		c.JSON(400, transport.HTTPError{Type: "Bad request", Title: "Ошибка", Data: err.Error()})
		return
	}

	//Ищем уже существующую
	filter := dc.VmFilter{
		Name:      vm.Name,
		IpAddress: vm.IpAddress,
	}
	existList, err := database.FindVm(filter)
	if err != nil {
		c.JSON(500, transport.HTTPError{Type: "Internal Server Error", Title: "Внутреняя ошибка сервера", Data: "Ошибка выполнения SQL команды"})
		return
	}

	if len(existList) == 0 {
		//Добавляем в БД
		vm, err := database.CreateVmInDb(vm)
		if err != nil {
			c.JSON(500, transport.HTTPError{Type: "Internal Server Error", Title: "Внутреняя ошибка сервера", Data: "Ошибка выполнения SQL команды"})
			return
		}
		//Добавляем в PowerDNS
		powerdns.AddVmToPDNS(vm)
		c.JSON(200, vm)
	} else {
		c.JSON(400, transport.HTTPError{
			Type:  "Bad request",
			Title: "Невозможно добавить ВМ",
			Data:  "ВМ с таким названием или IP адресом уже существует",
		})
		return
	}

}
func UpdateVm(c *gin.Context) {
}
func RemoveVm(c *gin.Context) {
	vm := dc.Vm{}
	err := c.BindJSON(&vm)
	if err != nil {
		log.Println("ERROR:", err.Error())
		c.JSON(400, transport.HTTPError{Type: "Bad request", Title: "Ошибка", Data: err.Error()})
		return
	}

	//Удаляем из БД
	vm, err = database.GetSingleVmFromDb(vm.Id)
	if err != nil {
		c.JSON(500, transport.HTTPError{Type: "Internal server error", Title: "Ошибка", Data: err.Error()})
	}
	if vm.Id == 0 {
		c.JSON(404, transport.HTTPError{Type: "Not Found", Title: "ВМ не найдена"})
	}
	err = database.DeleteVmFromDb(vm)
	if err != nil {
		c.JSON(500, transport.HTTPError{Type: "Internal server error", Title: "Ошибка", Data: err.Error()})
	}

	//Удаляем их DNS
	powerdns.RemoveVmToPDNS(vm)

	c.JSON(200, "Success")
}
