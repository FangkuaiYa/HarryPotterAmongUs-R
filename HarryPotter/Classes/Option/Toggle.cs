using AmongUs.Data;

namespace HarryPotter.CustomOption;

public class CustomToggleOption : CustomOption
{
    public CustomToggleOption(int id, string name, bool value = true) : base(id, name,
        CustomOptionType.Toggle,
        value)
    {
        Format = val =>
            (bool)val ? "optionOn".Translate() :
            "optionOff".Translate();
    }

    public bool Get()
    {
        return (bool)Value;
    }

    public void Toggle()
    {
        Set(!Get());
    }

    public override void OptionCreated()
    {
        base.OptionCreated();
        var tgl = Setting.Cast<ToggleOption>();
        tgl.CheckMark.enabled = Get();
    }
}