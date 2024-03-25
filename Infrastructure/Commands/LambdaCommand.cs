using GameOfLifeMVVM.Infrastructure.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeMVVM.Infrastructure.Commands
{
    class LambdaCommand : BaseCommand
    {
        private readonly Action<object> _Execute;
        private readonly Func<object, bool> _CanExecute;
        public LambdaCommand(Action<object> Execute, Func<object, bool> CanExectue = null) 
        {
            _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            _CanExecute = CanExectue;
        }

        public override bool CanExecute(object? parameter) => _CanExecute?.Invoke(parameter) ?? true;

        public override void Execute(object? parameter) => _Execute(parameter);
    }
}
