#!/bin/bash

if [ `ls  /App/toscan/* 2>/dev/null | wc -l ` -eq 0  ]; then
    echo "No files present to scan"
    exit -1
fi
if [ `ls  /App/toscan/*.tar.gz 2>/dev/null | wc -l ` -gt 0 ]; then
    tar xvzf /App/toscan/*.tar.gz -C /App/toscan
fi
if [ `ls  /App/toscan/*.tar.gz 2>/dev/null | wc -l ` -gt 0 ]; then
    tar xvzf /App/toscan/*.tgz  -C /App/toscan
fi
if [ `ls  /App/toscan/*.zip 2>/dev/null | wc -l ` -gt 0 ]; then
    unzip -o /App/toscan/\*.zip  -d /App/toscan
fi

# Run the .net program
dotnet scanner-jeff.dll