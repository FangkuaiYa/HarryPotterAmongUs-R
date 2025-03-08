namespace HarryPotter.CustomOption
{
    public class CustomHeaderOption : CustomOption
    {
        public CustomHeaderOption(int id, string name) : base(id, name, CustomOptionType.Header, 0)
        {
        }

        public override void OptionCreated()
        {
            base.OptionCreated();
            Setting.Cast<ToggleOption>().TitleText.text = Name;
        }
    }
}