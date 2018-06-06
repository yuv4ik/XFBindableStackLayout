using System.Collections;
using Xamarin.Forms;

namespace XFBindableStackLayout
{
    public class BindableStackLayout : StackLayout
    {
        readonly Label header;

        public BindableStackLayout()
        {
            header = new Label();
            Children.Add(header);
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(BindableStackLayout),
                                    propertyChanged: (bindable, oldValue, newValue) => ((BindableStackLayout)bindable).PopulateItems());

        public DataTemplate ItemDataTemplate
        {
            get { return (DataTemplate)GetValue(ItemDataTemplateProperty); }
            set { SetValue(ItemDataTemplateProperty, value); }
        }
        public static readonly BindableProperty ItemDataTemplateProperty =
            BindableProperty.Create(nameof(ItemDataTemplate), typeof(DataTemplate), typeof(BindableStackLayout));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(BindableStackLayout),
                                    propertyChanged: (bindable, oldValue, newValue) => ((BindableStackLayout)bindable).PopulateHeader());

        void PopulateItems()
        {
            if (ItemsSource == null) return;
            foreach (var item in ItemsSource)
            {
                var itemTemplate = ItemDataTemplate.CreateContent() as View;
                itemTemplate.BindingContext = item;
                Children.Add(itemTemplate);
            }
        }

        void PopulateHeader() => header.Text = Title;
    }
}
