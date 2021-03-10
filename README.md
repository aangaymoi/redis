# Redis example on windows.

```
public interface ICacheProvider
{
    bool Set<T>(object key, T value) where T : class;
    bool Set(object key, string value);
    bool Set(object key, long value);

    T Get<T>(object key);

    string Get(object key);

    long? GetLong(object key);

    bool Remove(object key);

    void ManualSave();
}
```
