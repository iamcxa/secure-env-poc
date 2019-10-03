<template>
    <div>
        <h1 class="mt-4">
            Base Test Page
        </h1>
        <label>Public Key</label><br />
        <textarea class="form-control" rows="3">N = {{public_key.N}}</textarea><br />
        <textarea class="form-control" rows="1">E = {{public_key.E}}</textarea>
        <hr />
        <label>Text Encrypt Test</label><br />
        <input v-model="text" class="form-control" placeholder="text you want to encrypt" />
        Encrypted:<br />
        <textarea class="form-control" rows="4">{{ text_encrypt }}</textarea>
    </div>
</template>
<script>

    import Connection from '../store/connection-module';
    export default {
        data() {
            return {
                text: '',
                connection: null,
            }
        },
        computed: {
            text_encrypt() {
                if (this.connection)
                    return this.connection.encrypt(this.text);
                else return '';
            },
            public_key() {
                if (this.connection)
                    return this.connection.getPublicKey();
                else return '';
            }
        },
        mounted: async function () {
            this.connection = await Connection.getInstance('/api/Encrypt');
        },
    }
</script>
