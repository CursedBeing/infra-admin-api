package transport

type HTTPError struct {
	Type  string `json:"type"`
	Title string `json:"title"`
	Data  string `json:"message"`
}
