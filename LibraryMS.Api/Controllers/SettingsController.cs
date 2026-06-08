using LibraryMS.Application.Features.Settings.Commands;
using LibraryMS.Application.Features.Settings.Queries;

namespace LibraryMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SettingsController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetSettings()
    {
        var result = await sender.Send(new GetSettingsQuery());
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{settingId:int}")]
    public async Task<IActionResult> UpdateSettings(int settingId, UpdateSettingCommand command)
    {
        if (settingId != command.SettingId)
            return BadRequest(Result.Failure("Mismatch Id"));

        var result = await sender.Send(command);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}