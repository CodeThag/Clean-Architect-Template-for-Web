using System;

namespace Application.Common.Interfaces
{
    public interface IDocumentService
    {
        void DeleteEmptyPreHistory(Guid processId);
        void ChangeState(Guid id, string nextState, string nextStateName);

    }
}
