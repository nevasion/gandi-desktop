using System;

namespace GandiDesktop.Gandi.Services.Hosting
{
    public class Operation
    {
        public int Id { get; private set; }
        public string Type { get; private set; }
        public string Source { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime LastUpdated { get; internal set; }
        public DateTime? Scheduled { get; private set; }
        public string Step { get; internal set; }

        public Operation(Mapping.Operation operation)
        {
            this.Id = operation.Id;
            this.Type = operation.Type;
            this.Source = operation.Source;
            this.Created = operation.Created;
            this.LastUpdated = operation.LastUpdated;
            this.Scheduled = operation.Scheduled;
            this.Step = operation.Step;
        }
    }
}