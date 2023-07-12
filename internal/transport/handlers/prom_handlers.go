package handlers

import (
	"github.com/gin-gonic/gin"
	prom2 "infra-api/internal/services/prom"
)

func GetVmList(c *gin.Context) {
	targets := []prom2.TargetQuery{}
	list := prom2.GetVmFromDb()
	var target = prom2.TargetQuery{}
	if len(list) > 0 {
		for _, item := range list {
			target = prom2.TargetQuery{
				Targets: []string{item.Name + "." + item.Domain + ":9273"},
				Labels:  map[string]string{"owner": "teamstr", "type": "vm", "location": "yar"},
			}
			targets = append(targets, target)
		}
	}
	c.JSON(200, targets)
}
