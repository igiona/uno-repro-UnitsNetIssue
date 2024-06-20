using System.Threading;
using Microsoft.UI.Xaml.Hosting;
using UnitsNet;

namespace UnitsNetsIssue.Presentation;

public partial record MainModel
{
    private INavigator _navigator;

    private readonly IDispatcher _dispatcher;

    public MainModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        IDispatcher dispatcher,
        INavigator navigator)
    {
        _navigator = navigator;
        _dispatcher = dispatcher;
        Title = "Main";
        Title += $" - {localizer["ApplicationName"]}";
        Title += $" - {appInfo?.Value?.Environment}";
    }

    public string? Title { get; }

    public IState<string> Name => State<string>.Value(this, () => string.Empty);

    public IState<DeviceStateModel> DeviceState => State<DeviceStateModel>.Value(this, () => new(false, null));


    public async Task GoToSecond()
    {
        var name = await Name;

        Task.Factory.StartNew(async () =>
        {
            await _dispatcher.ExecuteAsync(async (ct) =>
            {
                var deviceState = await DeviceState ?? throw new InvalidOperationException("Unable to get the current model state");
                await DeviceState.Update(_ => deviceState with { BatteryStateOfCharge = Ratio.FromPercent(Random.Shared.Next(0, 100)) }, ct);
            }, CancellationToken.None);
        });

        //var deviceState = await DeviceState ?? throw new InvalidOperationException("Unable to get the current model state");
        //await DeviceState.Update(_ => deviceState with { OperationIsInProgress = true, BatteryStateOfCharge = Ratio.FromPercent(100) }, default);


        //await _navigator.NavigateViewModelAsync<SecondModel>(this, data: new Entity(name!));
    }

}
