<template>
    <div>
        <h1 class="mt-4"> User Test Page </h1>
        <h2>User List Data</h2>
        <textarea class="form-control" rows="8">{{JSON.stringify(UserData, null,2)}}</textarea><br />
        <button class="btn btn-default btn-primary" v-on:click="getUserList">Get User Data</button><br /><br />
        <hr />
        <h2>Insert User</h2>
        <label>Account</label>
        <input type="text" v-model="NewUserData.Account" class="form-control" />
        <label>Password</label>
        <input type="text" v-model="NewUserData.Password" class="form-control" />
        <label>Name</label>
        <input type="text" v-model="NewUserData.Name" class="form-control" />
        <label>Email</label>
        <input type="text" v-model="NewUserData.Email" class="form-control" />
        <label>Telephone</label>
        <input type="text" v-model="NewUserData.Telephone" class="form-control" /><br />
        <button class="btn btn-default btn-primary" v-on:click="insertUser">Insert</button><br /><br />
        <hr />
        <h2>Update User</h2>
        <label>UserSN</label>
        <input type="number" v-model.numbr="UpdateUserSN" class="form-control" />
        <label>Name</label>
        <input type="text" v-model="UpdateUserData.Name" class="form-control" />
        <label>Email</label>
        <input type="text" v-model="UpdateUserData.Email" class="form-control" />
        <label>Telephone</label>
        <input type="text" v-model="UpdateUserData.Telephone" class="form-control" /><br />
        <button class="btn btn-default btn-primary" v-on:click="updatetUser">Update</button><br /><br />
        <hr />
        <h2>Delete User</h2>
        <label>UserSN</label>
        <input type="number" v-model.numbr="DeleteUserSN" class="form-control" /><br />
        <button class="btn btn-default btn-primary" v-on:click="deleteUser">Delete</button><br /><br />
        <br /><br />
    </div>
</template>
<script>
    import Connection from '../store/connection-module';
    export default {
        data() {
            return {
                connection: null,
                UserData: [],
                NewUserData: {
                    Account: 'new Account',
                    Password: 'new Password',
                    Name: 'new Name',
                    Email: 'new Email',
                    Telephone: 'new Telephone'
                },
                UpdateUserSN: 2,
                UpdateUserData: {
                    Name: 'update Name',
                    Email: 'update Email',
                    Telephone: 'update Telephone'
                },

                DeleteUserSN: 2,
            }
        },
        mounted: async function () {
            this.connection = await Connection.getInstance('/api/Encrypt');
        },
        methods: {
            getUserList: async function () {
                const response = await this.connection.get('/api/User');
                this.UserData = response.data;
            },

            insertUser: async function () {
                var postBody = Object.assign({}, this.NewUserData);
                var postOption = { encrypt: 'all' };
                const response = await this.connection.put('/api/EncryptUser', postBody, postOption);
                if (response && response.state) alert('Scuucess');
                else alert('Faild for ' + response.message);
            },
            updatetUser: async function () {
                var postBody = Object.assign({}, this.UpdateUserData);
                var postOption = { encrypt: ['Name', 'Email', 'Telephone'] };
                const response = await this.connection.post(`/api/EncryptUser/${this.UpdateUserSN}`, postBody, postOption);
                if (response && response.state) alert('Scuucess');
                else alert('Faild for ' + response.message);
            },

            deleteUser: async function () {
                const response = await this.connection.delete(`/api/User/${this.DeleteUserSN}`);
                if (response && response.state) alert('Scuucess');
                else alert('Faild for ' + response.message);
            },

        }
    }
</script>
