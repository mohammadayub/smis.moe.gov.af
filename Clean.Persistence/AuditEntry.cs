using Clean.Domain.Entity.au;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clean.Persistence
{
    public class AuditEntry
    {
        public EntityEntry Entry { get; }
        public string TableName { get; set; }
        public int UserId { get; set; }
        public int OperationTypeId { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();


        public bool HasTemporaryProperties => TemporaryProperties.Any();


        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public Audit ToAudit()
        {
            var audit = new Audit();


            audit.RecordId = JsonConvert.SerializeObject(KeyValues);
            audit.OperationDate = DateTime.Now;
            audit.DbContextObject = Entry.Context.GetType().Name;
            audit.DbObjectName = TableName;
            audit.UserId = UserId;
            audit.OperationTypeId = OperationTypeId;

            audit.ValueBefore = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues);
            audit.ValueAfter = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues);


            return audit;

        }
    }
}
