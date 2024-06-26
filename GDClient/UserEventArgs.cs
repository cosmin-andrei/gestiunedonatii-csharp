﻿namespace GDClient;

public enum UserEvent
{
    newDonation, newDonatie
} ;

public class UserEventArgs : EventArgs
{
    private readonly UserEvent userEvent;
    private readonly Object data;

    public UserEventArgs(UserEvent userEvent, object data)
    {
        this.userEvent = userEvent;
        this.data = data;
    }

    public UserEvent UserEventType
    {
        get { return userEvent; }
    }

    public object Data
    {
        get { return data; }
    }
}