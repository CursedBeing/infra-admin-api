package powerdns

type Rrsets struct {
	Sets []Rrset `json:"rrsets"`
}

type Rrset struct {
	Name       string       `json:"name"`
	Type       string       `json:"type"`
	Ttl        int64        `json:"ttl"`
	ChangeType string       `json:"changetype"`
	Records    []RecordList `json:"records"`
}

type RecordList struct {
	Content  string `json:"content"`
	Disabled bool   `json:"disabled"`
}
