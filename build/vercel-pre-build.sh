#!/bin/bash

# setup dotnet 3.1 on Amazon Linux 2 container. See https://alanta.nl/posts/2021/11/jamstack-on-dotnet-with-vercel
rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm
yum install dotnet-sdk-3.1 -y
