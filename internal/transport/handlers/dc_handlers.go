package handlers

import (
	"github.com/gin-gonic/gin"
	"infra-api/internal/database"
	"infra-api/internal/models/dc"
)

func GetDatacenters(c *gin.Context) {
	var list []dc.Datacenter
	list = database.GetDatacentersFromDb()
	c.JSON(200, list)
}

func CreateDatacenter(c *gin.Context) {
	datacenter := dc.Datacenter{}
	var err = c.BindJSON(&datacenter)
	if err != nil {
		c.JSON(400, err.Error())
		return
	}

	result, err := database.CreateDatacenterInDb(datacenter)
	if err != nil {
		c.JSON(500, "Internal server error:"+err.Error())
		return
	}

	c.JSON(200, result)
}

func UpdateDatacenter(c *gin.Context) {
	datacenter := dc.Datacenter{}
	var err = c.BindJSON(&datacenter)
	if err != nil {
		c.JSON(400, err.Error())
		return
	}

	result, err := database.UpdateDatacenterInDb(datacenter)
	if err != nil {
		c.JSON(500, "Internal server error:"+err.Error())
		return
	}

	c.JSON(200, result)
}

func DeleteDatacenter(c *gin.Context) {
	datacenter := dc.Datacenter{}

	var err = c.BindJSON(&datacenter)
	if err != nil {
		c.JSON(400, err.Error())
		return
	}

	err = database.DeleteDatacenterInDb(datacenter)
	if err != nil {
		c.JSON(500, "Internal server error:"+err.Error())
		return
	}

	c.JSON(200, "")
}
