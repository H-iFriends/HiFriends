namespace IRC;

public enum MessageType {
	PRIVMSG,
	NOTICE,
	JOIN,
	PART,
	QUIT,
	NICK,
	MODE,

	// Server messages
	ERR_NEEDMOREPARAMS = 461,
	ERR_INVITEONLYCHAN = 473,
	ERR_BANNEDFROMCHAN = 474,
	ERR_BADCHANNELKEY = 475,
	ERR_CHANNELISFULL = 471,
	ERR_NOSUCHCHANNEL = 403,
	ERR_TOOMANYCHANNELS = 405,
	ERR_TOOMANYTARGETS = 407,
	ERR_UNAVAILRESOURCE = 437,
	ERR_NONICKNAMEGIVEN = 431,
	ERR_ERRONEUSNICKNAME = 432,
	ERR_NICKNAMEINUSE = 433,
	ERR_NICKCOLLISION = 436,
	ERR_RESTRICTED = 484,
	ERR_NORECIPIENT = 411,
	ERR_CANNOTSENDTOCHAN = 404,
	ERR_NOTEXTTOSEND = 412,
	ERR_NOTOPLEVEL = 413,
	ERR_WILDTOPLEVEL = 414,
	ERR_NOTONCHANNEL = 442,

	// RPL_
	RPL_TOPIC = 332,
	RPL_AWAY = 301
}