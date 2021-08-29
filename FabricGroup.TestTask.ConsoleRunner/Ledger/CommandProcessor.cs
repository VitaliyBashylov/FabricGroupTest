using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FabricGroup.TestTask.ConsoleRunner.Ledger
{
    public class CommandProcessor
    {
        private readonly IOutput _output;
        private readonly Dictionary<string, Func<ILedgerCommand>> _factories = new();

        public CommandProcessor(IOutput output)
        {
            _output = output;
        }

        public void RegisterCommand(string name, Func<ILedgerCommand> factory) => 
            _factories.Add(name.ToLowerInvariant(), factory);

        public IEnumerable<ILedgerCommand> Load(IEnumerable<string> input)
        {
            return input.Select(LoadCommand);
        }

        private ILedgerCommand LoadCommand(string input)
        {
            var index = input.IndexOf(" ", StringComparison.Ordinal);
            var command = input.Substring(0, index).ToLowerInvariant();
            if (!_factories.ContainsKey(command))
                throw new ApplicationException($"unknown command {command} in {input}");

            return _factories[command]();
        }

        public async Task Execute(IEnumerable<ILedgerCommand> commands)
        {
            var context = new LedgerContext();
            foreach (var lc in commands)
            {
                await lc.Execute(context, _output);
            }
        }
    }
}