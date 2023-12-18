using Application.Menus.Commands.CreateMenu;
using Contracts.Menus;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/hosts/{hostId}/[controller]")]
public class MenusController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public MenusController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMenu(CreateMenuRequest request, string hostId)
    {
        var result = await _mediator.Send(_mapper.Map<CreateMenuCommand>((request, hostId)));

        return result.Match(
            createResult => Ok(_mapper.Map<MenuResponse>(createResult)),
            errors => Problem(errors));
    }
}
