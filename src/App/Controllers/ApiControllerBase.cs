﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

[ApiController]
[Route(("api/[controller]"))]
public class ApiControllerBase : ControllerBase
{
    private ISender _mediator = null;
    private IMapper _mapper = null;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetRequiredService<IMapper>();
}