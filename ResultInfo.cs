class ResultInfo
{
    private string _filename;
    private string _reason;

    public ResultInfo(string filename, string reason)
    {
        _filename = filename;
        _reason = reason;
    }

    public string Filename
    {
        get { return _filename; }
    }

    public string Reason
    {
        get { return _reason; }
    }
}