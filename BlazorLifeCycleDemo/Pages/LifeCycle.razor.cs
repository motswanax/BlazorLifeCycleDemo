using Microsoft.AspNetCore.Components;

namespace BlazorLifeCycleDemo.Pages;

public partial class LifeCycle : IDisposable
{
    [Parameter, EditorRequired]
    public string? Message { get; set; }

    private Guid _id = Guid.NewGuid();
    private int _renderCount = 0;
    private DateTime _initializedAt;

    // 1st - Entry Point
    public override Task SetParametersAsync(ParameterView parameters)
    {
        Console.WriteLine(nameof(SetParametersAsync));
        return base.SetParametersAsync(parameters);
    }

    // 2nd - Init - component is initialized to be rendered on the browser.
    protected override void OnInitialized()
    {
        Console.WriteLine(nameof(OnInitialized));
        _initializedAt = DateTime.Now;
    }

    protected override Task OnInitializedAsync()
    {
        Console.WriteLine(nameof(OnInitializedAsync));
        _initializedAt = DateTime.Now;
        return base.OnInitializedAsync();
    }

    // 3rd - Parameter changes - when a parent changes parameter values
    protected override void OnParametersSet()
    {
        Console.WriteLine($"{nameof(OnParametersSet)}: Message - {Message}");
    }

    // 4th - Rendering decision
    protected override bool ShouldRender()
    {
        Console.WriteLine(nameof(ShouldRender));
        return true;
    }

    // 5th - After render
    protected override void OnAfterRender(bool firstRender)
    {
        _renderCount++;
        Console.WriteLine($"{nameof(OnAfterRender)} (firstRender = {firstRender}) render count: {_renderCount}");
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        //_renderCount++;
        Console.WriteLine($"{nameof(OnAfterRenderAsync)} (firstRender = {firstRender}) render count: {_renderCount}");
        return base.OnAfterRenderAsync(firstRender);
    }

    public void Dispose()
    {
        Console.WriteLine($"{nameof(Dispose)} called - busy cleaning up.");
    }

    private void ForceRerender()
    {
        Console.WriteLine("User clicked button and StateHasChanged() is called.");
        StateHasChanged();
    }
}
