using AmongUs.Data;

namespace HarryPotter.CustomOption;

public class CustomToggleOption : CustomOption
{
    public CustomToggleOption(int id, string name, bool value = true) : base(id, name,
        CustomOptionType.Toggle,
        value)
    {
        Format = val =>
            (bool)val ? DataManager.Settings.Language.CurrentLanguage == SupportedLangs.SChinese ? "����" : "On" :
            DataManager.Settings.Language.CurrentLanguage == SupportedLangs.SChinese ? "�ر�" : "Off";
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