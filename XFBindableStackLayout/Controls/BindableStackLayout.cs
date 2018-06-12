using System;
using System.Collections;
using System.Windows.Input;
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

            ItemSelectedCommand = new Command<object>(item =>
            {
                SelectedItem = item;
            });
        }

        public event EventHandler<SelectedItemChangedEventArgs> SelectedItemChanged;

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IEnumerable),
            typeof(BindableStackLayout),
            propertyChanged: (bindable, oldValue, newValue) => ((BindableStackLayout)bindable).PopulateItems()
        );

        public DataTemplate ItemDataTemplate
        {
            get { return (DataTemplate)GetValue(ItemDataTemplateProperty); }
            set { SetValue(ItemDataTemplateProperty, value); }
        }
        public static readonly BindableProperty ItemDataTemplateProperty = BindableProperty.Create(
            nameof(ItemDataTemplate), typeof(DataTemplate), typeof(BindableStackLayout)
        );

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title), typeof(string),
            typeof(BindableStackLayout),
            propertyChanged: (bindable, oldValue, newValue) => ((BindableStackLayout)bindable).PopulateHeader()
        );

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static BindableProperty SelectedItemProperty = BindableProperty.Create(
            propertyName: "SelectedItem",
            returnType: typeof(object),
            declaringType: typeof(BindableStackLayout),
            defaultValue: null,
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: OnSelectedItemChanged
        );

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsView = (BindableStackLayout)bindable;
            if (newValue == oldValue)
                return;

            itemsView.SetSelectedItem(newValue);
        }

        protected virtual void SetSelectedItem(object selectedItem)
        {
            var handler = SelectedItemChanged;
            if (handler != null)
                handler(this, new SelectedItemChangedEventArgs(selectedItem));
        }

        void PopulateItems()
        {
            if (ItemsSource == null) return;
            foreach (var item in ItemsSource)
            {
                Children.Add(GetItemView(item));
            }
        }

        void PopulateHeader() => header.Text = Title;

        protected virtual View GetItemView(object item)
        {
            var content = ItemDataTemplate.CreateContent();

            var view = content as View;
            if (view == null)
                return null;

            view.BindingContext = item;

            var gesture = new TapGestureRecognizer
            {
                Command = ItemSelectedCommand,
                CommandParameter = item
            };

            AddGesture(view, gesture);

            return view;
        }

        protected readonly ICommand ItemSelectedCommand;

        protected void AddGesture(View view, TapGestureRecognizer gesture)
        {
            view.GestureRecognizers.Add(gesture);

            var layout = view as Layout<View>;

            if (layout == null)
                return;

            foreach (var child in layout.Children)
                AddGesture(child, gesture);
        }
    }
}