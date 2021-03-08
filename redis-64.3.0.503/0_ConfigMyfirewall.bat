netsh advfirewall firewall delete rule name="REDIS"
netsh advfirewall firewall add rule name="REDIS" dir=in action=allow protocol=tcp localport=6379,6380,6381 profile=any
@pause
