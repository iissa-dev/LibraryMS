using LibraryMS.Application.Features.Settings.Commands;
using LibraryMS.Application.Features.Settings.Queries;

namespace LibraryMS.Api.Controllers;

[Authorize("Admin,Employee")]
public class SettingsController(ISender sender) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetSettings()
    {
        var result = await sender.Send(new GetSettingsQuery());
        return HandleResult(result);
    }

    [HttpPut("{settingId:int}")]
    public async Task<IActionResult> UpdateSettings(int settingId, UpdateSettingCommand command)
    {
        if (settingId != command.SettingId)
            return BadRequest(Result.Failure("Mismatch Id"));

        var result = await sender.Send(command);
        return HandleResult(result);
    }
}