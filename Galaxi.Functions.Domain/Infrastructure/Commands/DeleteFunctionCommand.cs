﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxi.Functions.Domain.Infrastructure.Commands
{
    public record DeleteFunctionCommand(int functionId)
          : IRequest<bool>;
}
