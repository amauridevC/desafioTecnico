
using System;

namespace VR.Domain.Interfaces
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
        DateTime? DataExclusao { get; set; }
        string ExcluidoPor { get; set; }
    }
}
