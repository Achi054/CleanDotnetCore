using System;
using System.Collections.Generic;

namespace HealthCheckRegister
{
    public class HealthCheckResponse
    {
        public string Status { get; set; }

        public IEnumerable<HealthStatus> Checks { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
