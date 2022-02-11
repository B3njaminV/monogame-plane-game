using System;
using System.Collections.Generic;
using System.Text;

namespace MyoSharp.Commands
{
    /// <summary>
    /// A delegate signature for executing Myo commands.
    /// </summary>
    /// <returns>Returns a <see cref="IMyoCommandResult"/> instance with the result of the command. Cannot be null.</returns>
    public delegate IMyoCommandResult MyoCommandDelegate();
}
