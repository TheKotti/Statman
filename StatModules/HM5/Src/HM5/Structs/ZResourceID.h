#pragma once

#include <stdafx.h>
#include "ZString.h"

class ZRuntimeResourceID
{
public:
	uint32_t m_IDHigh;
	uint32_t m_IDLow;
};

class ZResourceID
{
public:
	ZString m_uri;
};
