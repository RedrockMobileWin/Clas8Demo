using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace HomeWorkDemo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        ObservableCollection<Book> list = new ObservableCollection<Book>();

        private async void SearchBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            string content = await GetHttpClient("https://api.douban.com/v2/book/search?q=" + SearchBox.Text);
            JObject jsonobj = JObject.Parse(content);
            string json = jsonobj["books"].ToString();
            list = JsonConvert.DeserializeObject<ObservableCollection<Book>>(json);
            ResultListView.ItemsSource = list;
        }
        private async Task<string> GetHttpClient(string uri)
        {
            string content = "";
            return await Task.Run(() =>
            {
                HttpClient httpClient = new HttpClient();
                System.Net.Http.HttpResponseMessage response;
                response = httpClient.GetAsync(new Uri(uri)).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                    content = response.Content.ReadAsStringAsync().Result;
                return content;
            });
        }
    }
}

public class Rootobject
{
    public int count { get; set; }
    public int start { get; set; }
    public int total { get; set; }
    public Book[] books { get; set; }
}

public class Book
{
    public Rating rating { get; set; }
    public string subtitle { get; set; }
    public string[] author { get; set; }
    public string pubdate { get; set; }
    public Tag[] tags { get; set; }
    public string origin_title { get; set; }
    public string image { get; set; }
    public string binding { get; set; }
    public object[] translator { get; set; }
    public string catalog { get; set; }
    public string pages { get; set; }
    public Images images { get; set; }
    public string alt { get; set; }
    public string id { get; set; }
    public string publisher { get; set; }
    public string isbn10 { get; set; }
    public string isbn13 { get; set; }
    public string title { get; set; }
    public string url { get; set; }
    public string alt_title { get; set; }
    public string author_intro { get; set; }
    public string summary { get; set; }
    public string price { get; set; }
    public Series series { get; set; }
}

public class Rating
{
    public int max { get; set; }
    public int numRaters { get; set; }
    public string average { get; set; }
    public int min { get; set; }
}

public class Images
{
    public string small { get; set; }
    public string large { get; set; }
    public string medium { get; set; }
}

public class Series
{
    public string id { get; set; }
    public string title { get; set; }
}

public class Tag
{
    public int count { get; set; }
    public string name { get; set; }
    public string title { get; set; }
}
