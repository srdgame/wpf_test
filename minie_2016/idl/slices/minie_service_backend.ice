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
			
			int add_user_group(string groupID, string userID);
			int add_role_group(string groupID, string roleID);
			int add_role_user(string userID, string roleID);
			
			int remove_user_group(string groupID, string userID);
			int remove_role_group(string groupID, string roleID);
			int remove_role_user(string userID, string roleID);
			
			int add_sys_group(sys_group_rpc group);
			int add_sys_user(sys_user_rpc user);
			int add_cm_node(cm_node_rpc node);
			int add_cm_entrance(cm_entrance_rpc entrance);

			int update_sys_group(sys_group_rpc group);
			int update_sys_user(sys_user_rpc user);
			int update_cm_node(cm_node_rpc node);
			int update_cm_entrance(cm_entrance_rpc entrance);

			sys_group_rpc get_sys_group(string groupID);
			sys_user_rpc get_sys_user(string userID);
			cm_node_rpc get_cm_node(string nodeID);
			cm_entrance_rpc get_cm_entrance(string entranceID);

			int remove_sys_group(string groupID);
			int remove_sys_user(string userID);
			int remove_cm_node(string nodeID);
			int remove_cm_entrance(string entranceID);
		};
	
	};
};