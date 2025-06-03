using FilmsAPI.Models;

namespace FilmsAPI.Controllers
{
    public class SequenceIdController
    {
        public int SequenceIdIncrement(int parmId)
        {
            SequenceId sequenceId = new SequenceId();

            sequenceId.Id = parmId;
            return ++sequenceId.Id;
        }
    }
}
