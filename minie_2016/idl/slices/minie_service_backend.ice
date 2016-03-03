#pragma once
#include <Ice/BuiltinSequences.ice>
#include <Ice/Identity.ice>
[["cpp:include:list"]]
#include "minie_service_base.ice"
module minie
{
	module irpc
	{
		interface BackendService;
		interface BackendAuth
		{
			BackendService* login_backend(string un, string pw) throws AuthError;
			BackendService* login_backend_by_token(string token) throws AuthError;
		};
		interface BackendService extends BaseService
		{
			sys_user_rpc get_current_user_info();
			sys_group_rpc_list get_groups();
			cm_node_rpc_list get_nodes();
			cm_node_category_rpc_list get_node_categories();
			sys_role_rpc_list get_roles(); 
			sys_permission_rpc_list get_permissions();
			cm_user_rpc_list find_app_user(string keywords);
			bool register_app_user_step1(string cellphone);
			cm_user_rpc register_app_user_step2(string cellphone, string password, int verifyCode)  throws VerificationError;
			
			int add_user_group(sys_user_rpc user, sys_group_rpc group);
			int add_role_group(sys_role_rpc role, sys_group_rpc group);
			int add_role_user(sys_role_rpc role, sys_user_rpc user);

		};
	
	};
};