package main

import (
	"github.com/gin-gonic/gin"
	"infra-api/internal/config"
	"infra-api/internal/transport/handlers"
)

var configuration config.Config

func main() {
	//gin.SetMode(gin.ReleaseMode)
	//Загружаем конфиг
	configuration = configuration.LoadConfig()
	//gin.SetMode(gin.ReleaseMode)
	//Устанавлиаем настройки роутинга
	router := gin.Default()
	v1 := router.Group("/api/v1")
	{
		v1.GET("/test", handlers.GetVms)

		v1.GET("/datacenters", handlers.GetDatacenters)
		dcRouter := v1.Group("/datacenter")
		{
			dcRouter.POST("/create", handlers.CreateDatacenter)
			dcRouter.POST("/update", handlers.UpdateDatacenter)
			dcRouter.POST("/remove", handlers.DeleteDatacenter)
		}
		v1.GET("/vms", handlers.GetVms)
		vmRouter := v1.Group("/vm")
		{
			vmRouter.POST("/find", handlers.FindVm)
			vmRouter.POST("/create", handlers.CreateVm)
			vmRouter.POST("/update", handlers.UpdateVm)
			vmRouter.POST("/remove", handlers.RemoveVm)
		}
		promRouter := v1.Group("/prom")
		{
			promRouter.GET("vm", handlers.GetVmList)
			promRouter.GET("hosts")
		}
	}

	//Запускаем!
	if configuration.Core.Listen == "" {
		configuration.Core.Listen = ":5000"
	}
	_ = router.Run(configuration.Core.Listen)
}
