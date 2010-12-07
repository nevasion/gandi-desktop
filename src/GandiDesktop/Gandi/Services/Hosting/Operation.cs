using System;

namespace GandiDesktop.Gandi.Services.Hosting
{
    public class Operation
    {
        public int Id { get; private set; }
        public OperationType Type { get; private set; }
        public string Source { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime LastUpdated { get; set; }
        public OperationStep Step { get; set; }

        public Operation(Mapping.Operation operation)
        {
            this.Id = operation.Id;
            this.Type = Converter.ToOperationType(operation.Type);
            this.Source = operation.Source;
            this.Created = operation.Created;
            this.LastUpdated = operation.LastUpdated;
            this.Step = Converter.ToOperationStep(operation.Step);
        }
    }
}