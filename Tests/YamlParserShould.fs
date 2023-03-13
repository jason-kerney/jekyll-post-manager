module PostMaster.Tests.``Yaml Parser``

open System
open FeldSpar.Framework
open FeldSpar.Framework.Verification
open PostMaster.Lib
open PostMaster.Lib.Helpers

let ``should parse Id, Date, and Time`` =
    Test(fun _ ->
        """
---
layout: post
title: A Title to be Placed Here
date: 2023-04-07 00:30
category: business
author: Jason
tags: ['Tag A', 'Tag B', 'Tag C', 'Tag D']
summary: A long summary about the post will be here and ignored
series: ""A Series name This field is optional and ignored""
---
"""
        |> trim
        |> YamlParser.parse
        |> expectsToBe {
            Id = "A Title to be Placed Here"
            DateTime = "2023-04-07 00:30" |> DateTime.Parse
        }
    )