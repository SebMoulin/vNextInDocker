using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEK.Recruit.Commons.Entities;

namespace TEK.Recruit.BusinessServices.Services.EnvironmentSetup
{
    public class EnvironmentSetupCoordinator : ICoordonateTasks<EnvironmentSetUpResult>
    {
        private readonly SortedList<int, IExecuteTask<EnvironmentSetUpResult>> _tasks;

        public EnvironmentSetupCoordinator()
        {
            _tasks = new SortedList<int, IExecuteTask<EnvironmentSetUpResult>>();
        }

        public void RegisterTask(int order, IExecuteTask<EnvironmentSetUpResult> task)
        {
            _tasks.Add(order, task);
        }

        public async Task<EnvironmentSetUpResult> StartProcess()
        {
            var parallelTasks = _tasks.Where(t => t.Value.CanRunInParallel);
            var sequencedTasks = _tasks.Where(t => !t.Value.CanRunInParallel);

            var token = new EnvironmentSetUpResult();

            foreach (var sequencedTask in sequencedTasks.OrderBy(t => t.Key))
            {
                var result = await sequencedTask.Value.Execute(token);
                token = result;
                if (!sequencedTask.Value.CanContinue(token))
                    break;
            }

            if (token.Success)
            {
                var tasksToExecute = new List<Task<EnvironmentSetUpResult>>(
                    parallelTasks.Select(t => t.Value.Execute(token)));
                await Task.WhenAll(tasksToExecute.ToArray());
                var results = tasksToExecute.Select(t => t.Result).ToList();
                if (results.Any(r => !r.Success))
                {
                    token = results.First(r => !r.Success);
                }
            }

            if (token.Success)
            {
                token.Message = "Candidate's coding excercise repository is ready";
            }

            return token;
        }
    }
}
