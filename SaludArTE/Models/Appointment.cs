using Newtonsoft.Json;
using System;

namespace SaludArTE.Models
{
    public class Appointment
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "start")]
        public DateTime Start { get; set; }

        [JsonProperty(PropertyName = "end")]
        public DateTime? End { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonIgnore]
        public Guid PatientId { get; set; }

        [JsonIgnore]
        public string PatientName { get; set; }
    }
}