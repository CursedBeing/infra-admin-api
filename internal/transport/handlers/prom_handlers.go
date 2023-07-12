package handlers

import (
	"github.com/gin-gonic/gin"
	"infra-api/internal/models/prom"
)

func GetVmList(c *gin.Context) {
	targets := []prom.TargetQuery{}
	list := prom.GetVmFromDb()
	var target = prom.TargetQuery{}
	if len(list) > 0 {
		for _, item := range list {
			target = prom.TargetQuery{
				Targets: []string{item.Name + "." + item.Domain + ":9273"},
				Labels:  map[string]string{"owner": "teamstr", "type": "vm", "location": "yar"},
			}
			targets = append(targets, target)
		}
	}
	c.JSON(200, targets)
}
