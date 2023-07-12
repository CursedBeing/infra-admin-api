package prom

type TargetQuery struct {
	Targets []string          `json:"targets"`
	Labels  map[string]string `json:"labels"`
}
