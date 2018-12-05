using System;

namespace JDA.Entities
{
    public enum TokenStatus
    {
        None = -1,
        NotProvided = 0,
        Valid = 1,
        Invalid = 2,
        Expired = 3,
        NotRequired = 4,
        NotEvaluated = 5
    }
}